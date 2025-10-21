using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SandSimulator2.Controls;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Multiplayer;

/// <summary>
/// Cliente UDP para sincronizar acciones y estado de la grilla con un servidor.
/// </summary>
/// <remarks>
/// Este cliente envía un <see cref="MessageType.Handshake"/> al conectar y, posteriormente,
/// transmite <see cref="PlaceAction"/> al servidor. También escucha mensajes entrantes para
/// aplicar acciones remotas o actualizar el estado de la grilla.
/// </remarks>
public class MultiplayerClient : IDisposable
{
    private readonly UdpClient _udpClient;
    private bool _isDisposed = false;
    private readonly GridManager _gridManager;
    private IPEndPoint _serverEndPoint;

    /// <summary>
    /// Crea una nueva instancia del cliente multijugador.
    /// </summary>
    /// <param name="gridManager">Administrador de grilla donde se aplicarán las actualizaciones recibidas.</param>
    public MultiplayerClient(GridManager gridManager)
    {
        _gridManager = gridManager;
        _udpClient = new UdpClient();
    }

    /// <summary>
    /// Conecta con el servidor y envía un mensaje de saludo (handshake).
    /// </summary>
    /// <param name="ipAddress">Dirección IP del servidor.</param>
    /// <param name="port">Puerto del servidor.</param>
    public void Connect(string ipAddress, int port)
    {
        _serverEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        _udpClient.Connect(_serverEndPoint);
        Console.WriteLine($"Connected to server at {_serverEndPoint}");

        // Enviar un mensaje de handshake al servidor
        var handshake = new NetworkMessage { MessageType = MessageType.Handshake };
        var settings = new JsonSerializerSettings();
        string message = JsonConvert.SerializeObject(handshake, settings);
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        _udpClient.Send(buffer, buffer.Length);
    }

    /// <summary>
    /// Envía de forma asíncrona una acción de colocación al servidor.
    /// </summary>
    /// <param name="action">Acción de colocación a enviar.</param>
    public async Task SendActionAsync(PlaceAction action)
    {
        if (_serverEndPoint == null)
        {
            Console.WriteLine("Not connected to a server.");
            return;
        }

        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        var payload = JsonConvert.SerializeObject(action, settings);
        var networkMessage = new NetworkMessage { MessageType = MessageType.PlaceAction, Payload = payload };

        string message = JsonConvert.SerializeObject(networkMessage);
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        await _udpClient.SendAsync(buffer, buffer.Length);
    }


    /// <summary>
    /// Inicia el bucle de escucha asíncrono para procesar mensajes entrantes.
    /// </summary>
    /// <remarks>
    /// Aplica <see cref="PlaceAction"/> recibidas en cola al <see cref="GridManager"/> y
    /// puede actualizar el estado completo de la grilla si se recibe <see cref="MessageType.GridState"/>.
    /// </remarks>
    public async Task StartListeningAsync()
    {
        try
        {
            while (true)
            {
                // Espera hasta recibir un datagrama
                UdpReceiveResult result = await _udpClient.ReceiveAsync();

                // Procesa el datagrama recibido
                string message = Encoding.UTF8.GetString(result.Buffer);
                var networkMessage = JsonConvert.DeserializeObject<NetworkMessage>(message);

                if (networkMessage != null)
                {
                    var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                    switch (networkMessage.MessageType)
                    {
                        case MessageType.PlaceAction:
                            var placeAction = JsonConvert.DeserializeObject<PlaceAction>(networkMessage.Payload, settings);
                            if (placeAction != null)
                            {
                                _gridManager.EnqueueAction(placeAction);
                            }
                            break;
                        case MessageType.GridState:
                            var gridState = JsonConvert.DeserializeObject<GridState>(networkMessage.Payload, settings);
                            if (gridState != null)
                            {
                                _gridManager.SetGridState(gridState);
                            }
                            break;
                    }
                }
            }
        }
        catch (ObjectDisposedException)
        {
            // Esta excepción es esperada cuando el listener se cierra.
            Console.WriteLine("Listener has been closed.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    /// <summary>
    /// Libera los recursos asociados al cliente y cierra el socket UDP.
    /// </summary>
    public void Dispose()
    {
        if (!_isDisposed)
        {
            _udpClient.Close();
            _udpClient.Dispose();
            _isDisposed = true;
        }
    }
}