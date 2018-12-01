using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace LCU.NET.Utils
{
    public interface ILSocket
    {
        event Action Closed;
        event Action<string> MessageReceived;

        bool Init(int port, string password);
        void Close();
    }

    public class LWebSocket : ILSocket
    {
        private WebSocket Socket;

        public event Action Closed;
        public event Action<string> MessageReceived;

        public bool Init(int port, string password)
        {
            Socket = new WebSocket($"wss://127.0.0.1:{port}/", "wamp");
            Socket.SetCredentials("riot", password, true);
            Socket.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;
            Socket.SslConfiguration.ServerCertificateValidationCallback = delegate { return true; };
            Socket.OnMessage += Socket_OnMessage;
            Socket.OnClose += Socket_OnClose;
            Socket.Connect();
            Socket.Send("[5, \"OnJsonApiEvent\"]");

            return Socket.IsAlive;
        }

        public void Close()
        {
            Socket?.Close();
        }

        private void Socket_OnClose(object sender, CloseEventArgs e) => Closed?.Invoke();

        private void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.IsText)
            {
                MessageReceived?.Invoke(e.Data);
            }
        }
    }
}
