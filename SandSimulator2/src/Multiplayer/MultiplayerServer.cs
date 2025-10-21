using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SandSimulator2.Controls;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Multiplayer;

/// <summary>
/// Servidor UDP que recibe acciones de los clientes, las aplica a la grilla y las reenvía al resto.
/// </summary>
public class MultiplayerServer : IDisposable
{
    private readonly UdpClient _listener;
    private readonly List<IPEndPoint> _clients = new();
    private readonly GridManager _gridManager;
    private bool _isDisposed = false;

    /// <summary>
    /// Crea una nueva instancia del servidor en el puerto especificado.
    /// </summary>
    /// <param name="port">Puerto UDP en el que escuchar.</param>
    /// <param name="gridManager">Administrador de grilla sobre el que se aplicarán acciones.</param>
    public MultiplayerServer(int port, GridManager gridManager)
    {
        _gridManager = gridManager;
        _listener = new UdpClient(port);
        Console.WriteLine($"Server started on port {port}. Waiting for messages...");
    }

    /// <summary>
    /// Inicia el bucle asíncrono de escucha para recibir y procesar datagramas.
    /// </summary>
    public async Task StartListeningAsync()
    {
        try
        {
            while (true)
            {
                UdpReceiveResult result = await _listener.ReceiveAsync();
                IPEndPoint clientEndPoint = result.RemoteEndPoint;
                string message = Encoding.UTF8.GetString(result.Buffer);

                var networkMessage = JsonConvert.DeserializeObject<NetworkMessage>(message);

                if (networkMessage != null)
                {
                    switch (networkMessage.MessageType)
                    {
                        case MessageType.Handshake:
                            if (!_clients.Contains(clientEndPoint))
                            {
                                _clients.Add(clientEndPoint);
                                Console.WriteLine($"New client connected: {clientEndPoint}");
                                // Envío del estado completo deshabilitado temporalmente:
                                // puede exceder el tamaño máximo de datagrama UDP (MTU) y causar WSAEMSGSIZE,
                                // rompiendo la simulación al conectar clientes con grillas grandes.
                                // await SendFullGridState(clientEndPoint);
                            }
                            break;
                        case MessageType.PlaceAction:
                            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                            var action = JsonConvert.DeserializeObject<PlaceAction>(networkMessage.Payload, settings);
                            if (action != null)
                            {
                                _gridManager.EnqueueAction(action);
                                await BroadcastActionAsync(action, clientEndPoint);
                            }
                            break;
                    }
                }
            }
        }
        catch (ObjectDisposedException)
        {
            Console.WriteLine("Server has been closed.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    /// <summary>
    /// Difunde una acción recibida a todos los clientes excepto al origen.
    /// </summary>
    /// <param name="action">Acción de colocación a difundir.</param>
    /// <param name="origin">Extremo remoto que originó la acción, para no reenviarle.</param>
    public async Task BroadcastActionAsync(PlaceAction action, IPEndPoint? origin)
    {
        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        var payload = JsonConvert.SerializeObject(action, settings);
        var networkMessage = new NetworkMessage { MessageType = MessageType.PlaceAction, Payload = payload };

        string message = JsonConvert.SerializeObject(networkMessage);
        byte[] buffer = Encoding.UTF8.GetBytes(message);

        foreach (var client in _clients)
        {

            if (!client.Equals(origin))
            {
                await _listener.SendAsync(buffer, buffer.Length, client);
            }
        }
    }

    /// <summary>
    /// Libera los recursos del socket UDP del servidor.
    /// </summary>
    public void Dispose()
    {
        if (!_isDisposed)
        {
            _listener.Close();
            _listener.Dispose();
            _isDisposed = true;
        }
    }
}