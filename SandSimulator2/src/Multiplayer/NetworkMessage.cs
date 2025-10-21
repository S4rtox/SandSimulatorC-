namespace SandSimulator2.Multiplayer
{
    /// <summary>
    /// Tipos de mensajes que pueden intercambiarse entre cliente y servidor.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Mensaje que describe una acción de colocación de elementos en la grilla.
        /// </summary>
        PlaceAction,
        /// <summary>
        /// Mensaje que contiene un volcado del estado completo de la grilla.
        /// </summary>
        GridState,
        /// <summary>
        /// Mensaje de saludo/negociación inicial para registrar el cliente en el servidor.
        /// </summary>
        Handshake
    }

    /// <summary>
    /// Contenedor genérico para mensajes de red enviados por UDP.
    /// </summary>
    public class NetworkMessage
    {
        /// <summary>
        /// Tipo de mensaje que indica cómo interpretar el contenido del <see cref="Payload"/>.
        /// </summary>
        public MessageType MessageType { get; set; }
        /// <summary>
        /// Carga útil serializada en JSON, cuyo tipo depende de <see cref="MessageType"/>.
        /// </summary>
        public string Payload { get; set; }
    }
}
