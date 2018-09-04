using LCU.NET.API_Models;
using LCU.NET.WAMP;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebSocketSharp;

namespace LCU.NET
{
    public enum EventType
    {
        Create,
        Update,
        Delete
    }

    public static class LeagueSocket
    {
        private static WebSocket Socket;
        private static IDictionary<Regex, Tuple<Type, Delegate>> Subscribers = new Dictionary<Regex, Tuple<Type, Delegate>>();

        public static bool DumpToDebug { get; set; }

        public static bool DebugMode { get; set; }

        public delegate void MessageHandlerDelegate<T>(EventType eventType, T data);
        
        internal static void Init(int port, string password)
        {
            if (File.Exists("log.txt"))
                File.Delete("log.txt");

            Socket = new WebSocket($"wss://127.0.0.1:{port}/", "wamp");
            Socket.SetCredentials("riot", password, true);
            Socket.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;
            Socket.SslConfiguration.ServerCertificateValidationCallback = delegate { return true; };
            Socket.OnMessage += Socket_OnMessage;
            Socket.OnClose += Socket_OnClose;
            Socket.Connect();
            Socket.Send("[5, \"OnJsonApiEvent\"]");

            Debug.WriteLine("WebSocket connected");
        }
        
        private static void Socket_OnClose(object sender, CloseEventArgs e)
        {
            LeagueClient.Default.Close();
        }

        internal static void Close()
        {
            Socket.Close();
            Socket = null;
        }

        private static Regex BuildRegex(string path) => new Regex($"^{Regex.Escape(path)}$");

        public static void Subscribe<T>(string path, MessageHandlerDelegate<T> action)
        {
            Subscribe(BuildRegex(path), action);
        }
        
        public static void Subscribe<T>(Regex regex, MessageHandlerDelegate<T> action)
        {
            Subscribers.Add(regex, new Tuple<Type, Delegate>(typeof(T), action));
        }

        public static void Unsubscribe(string path)
        {
            Unsubscribe(BuildRegex(path));
        }

        public static void Unsubscribe(Regex regex)
        {
            if (Subscribers.TryGetValue(regex, out var v))
            {
                Subscribers.Remove(regex);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        private static void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            if (DumpToDebug)
                Debug.WriteLine(e.Data);

            var ev = JsonApiEvent.Parse(e.Data);
            
            if (!ev.Equals(default(JsonApiEvent)))
            {
                if (LeagueClient.Default.Proxy != null)
                    ev = LeagueClient.Default.Proxy.Handle(ev);

                HandleEvent(ev);
            }
        }

        public static void HandleEvent(JsonApiEvent @event)
        {
            foreach (var item in Subscribers.Where(o => o.Key.IsMatch(@event.URI)))
            {
                object data;

                try
                {
                    data = @event.GetData(item.Value.Item1);
                }
                catch (InvalidCastException)
                {
                    //TODO Do something?
                    continue;
                }

                item.Value.Item2.DynamicInvoke(@event.EventType, data);
            }
        }
    }
}
