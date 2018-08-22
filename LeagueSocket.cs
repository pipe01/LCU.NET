using LCU.NET.API_Models;
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
    internal enum MessageTypes
    {
        WELCOME,
        PREFIX,
        CALL,
        CALLRESULT,
        CALLERROR,
        SUBSCRIBE,
        UNSUBSCRIBE,
        PUBLISH,
        EVENT
    }

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

        public delegate void MessageHandlerDelegate<T>(EventType eventType, T data);
        
#pragma warning disable RCS1163 // Unused parameter.
        internal static void Init(int port, string password)
        {
            if (File.Exists("log.txt"))
                File.Delete("log.txt");

            Socket = new WebSocket($"wss://127.0.0.1:{port}/", "wamp");
            Socket.SetCredentials("riot", password, true);
            Socket.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;
            Socket.SslConfiguration.ServerCertificateValidationCallback = (b, o, O, B) => true;
            Socket.OnMessage += Socket_OnMessage;
            Socket.OnClose += Socket_OnClose;
            Socket.Connect();
            Socket.Send("[5, \"OnJsonApiEvent\"]");
            //Socket.Send("[4]");
            
            Debug.WriteLine("WebSocket connected");
        }
#pragma warning restore RCS1163 // Unused parameter.
        
        private static void Socket_OnClose(object sender, CloseEventArgs e)
        {
            LeagueClient.Close();
        }

        public static void Subscribe<T>(string path, MessageHandlerDelegate<T> action)
        {
            Subscribe(new Regex($"^{Regex.Escape(path)}$"), action);
        }
        
        public static void Subscribe<T>(Regex regex, MessageHandlerDelegate<T> action)
        {
            Subscribers.Add(regex, new Tuple<Type, Delegate>(typeof(T), action));
            //Socket.Send($"[1, \"{regex}\"]");
        }

        private static void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            if (DumpToDebug)
                Debug.WriteLine(e.Data);
            
            var obj = JArray.Parse(e.Data);
            
            if (obj.Count > 1 && obj[1].Value<string>() == "OnJsonApiEvent")
            {
                string uri = obj[2]["uri"].Value<string>();
                string eventType = obj[2]["eventType"].Value<string>();

                foreach (var item in Subscribers.Where(o => o.Key.IsMatch(uri)))
                {
                    object data;

                    try
                    {
                        data = obj[2]["data"].ToObject(item.Value.Item1);
                    }
                    catch (InvalidCastException)
                    {
                        //TODO Do something?
                        continue;
                    }

                    if (!Enum.TryParse<EventType>(eventType, out var @event))
                        throw new Exception("Invalid event type found: " + eventType);

                    item.Value.Item2.DynamicInvoke(@event, data);
                }
            }
        }
    }
}
