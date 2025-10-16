using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SandSimulator2.Controls;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Multiplayer;

public class MultiplayerClient : IDisposable
{
    private readonly UdpClient _udpClient;
    private bool _isDisposed = false;
    private readonly GridManager _gridManager;
    private IPEndPoint _serverEndPoint;

    // The constructor binds the listener to a specific port
    public MultiplayerClient(GridManager gridManager)
    {
        _gridManager = gridManager;
        _udpClient = new UdpClient();
    }

    public void Connect(string ipAddress, int port)
    {
        _serverEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        _udpClient.Connect(_serverEndPoint);
        Console.WriteLine($"Connected to server at {_serverEndPoint}");

        // Send a handshake message to the server
        var handshake = new NetworkMessage { MessageType = MessageType.Handshake };
        var settings = new JsonSerializerSettings();
        string message = JsonConvert.SerializeObject(handshake, settings);
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        _udpClient.Send(buffer, buffer.Length);
    }

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


    // The main instance method to run the listening loop
    public async Task StartListeningAsync()
    {
        try
        {
            while (true)
            {
                // Wait for a datagram to arrive
                UdpReceiveResult result = await _udpClient.ReceiveAsync();

                // Process the received datagram
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
            // This exception is expected when the listener is closed, so we can ignore it.
            Console.WriteLine("Listener has been closed.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    // Implement IDisposable to properly close the UdpClient
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