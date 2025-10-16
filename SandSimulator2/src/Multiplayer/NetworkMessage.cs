namespace SandSimulator2.Multiplayer
{
    public enum MessageType
    {
        PlaceAction,
        GridState,
        Handshake
    }

    public class NetworkMessage
    {
        public MessageType MessageType { get; set; }
        public string Payload { get; set; }
    }
}

