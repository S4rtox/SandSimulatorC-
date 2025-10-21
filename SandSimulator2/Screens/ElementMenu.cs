using System;

namespace SandSimulator2.Screens
{
    /// <summary>
    /// Menú de inicio para alojar o unirse a una partida multijugador.
    /// </summary>
    partial class ElementMenu
    {
        /// <summary>
        /// Se dispara al pulsar el botón de Host con el puerto seleccionado.
        /// </summary>
        public event Action<int> HostClicked;
        /// <summary>
        /// Se dispara al pulsar el botón de Join con IP y puerto seleccionados.
        /// </summary>
        public event Action<string, int> JoinClicked;

        partial void CustomInitialize()
        {
            Ip.Text = "127.0.0.1";
            Port.Text = "7777";

            Host.Click += (sender, args) =>
            {
                if (int.TryParse(Port.Text, out var portNumber))
                {
                    HostClicked?.Invoke(portNumber);
                }
                else
                {
                    HostClicked?.Invoke(7777); // Puerto por defecto
                }
            };

            Join.Click += (sender, args) =>
            {
                var ipAddress = string.IsNullOrWhiteSpace(Ip.Text) ? "127.0.0.1" : Ip.Text;
                if (int.TryParse(Port.Text, out var portNumber))
                {
                    JoinClicked?.Invoke(ipAddress, portNumber);
                }
                else
                {
                    JoinClicked?.Invoke(ipAddress, 7777); // Puerto por defecto
                }
            };
        }
    }
}
