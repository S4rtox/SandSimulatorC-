using System;

namespace SandSimulator2.Screens
{
    partial class ElementMenu
    {
        public event Action<int> HostClicked;
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
                    HostClicked?.Invoke(7777); // Default port
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
                    JoinClicked?.Invoke(ipAddress, 7777); // Default port
                }
            };
        }
    }
}
