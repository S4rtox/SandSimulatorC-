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

public class MultiplayerServer : IDisposable
{
    private readonly UdpClient _listener;
    private readonly List<IPEndPoint> _clients = new();
    private readonly GridManager _gridManager;
    private bool _isDisposed = false;

    public MultiplayerServer(int port, GridManager gridManager)
    {
        _gridManager = gridManager;
        _listener = new UdpClient(port);
        Console.WriteLine($"Server started on port {port}. Waiting for messages...");
    }

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
                                await SendFullGridState(clientEndPoint);
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

    private async Task SendFullGridState(IPEndPoint clientEndPoint)
    {
        var gridState = _gridManager.GetGridState();
        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        var payload = JsonConvert.SerializeObject(gridState, settings);
        var networkMessage = new NetworkMessage { MessageType = MessageType.GridState, Payload = payload };

        string message = JsonConvert.SerializeObject(networkMessage);
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        await _listener.SendAsync(buffer, buffer.Length, clientEndPoint);
    }

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