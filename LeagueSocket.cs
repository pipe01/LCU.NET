using LCU.NET.API_Models;
using LCU.NET.Utils;
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
using System.Threading;
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

    public delegate void MessageHandlerDelegate<T>(EventType eventType, T data);

    public interface ILeagueSocket
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        event Action Closed;

        bool DumpToDebug { get; set; }

        void Connect(int port, string password);
        void Close();

        void Subscribe<T>(string path, MessageHandlerDelegate<T> action);
        void Subscribe<T>(Regex regex, MessageHandlerDelegate<T> action);
        void Unsubscribe(string path);
        void Unsubscribe(Regex regex);
    }

    public class LeagueSocket : ILeagueSocket
    {
        private readonly ILSocket Socket;
        private readonly IDictionary<Regex, Tuple<Type, Delegate>> Subscribers = new Dictionary<Regex, Tuple<Type, Delegate>>();

        public bool DumpToDebug { get; set; }
        public bool IsPlaying { get; private set; }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event Action Closed;

        public LeagueSocket(ILSocket socket)
        {
            if (File.Exists("log.txt"))
                File.Delete("log.txt");

            Socket = socket;
            socket.Closed += () => Closed?.Invoke();
            socket.MessageReceived += Socket_MessageReceived;

            Debug.WriteLine("WebSocket connected");
        }

        public void Connect(int port, string password)
        {
            Socket.Init(port, password);
        }

        private void Socket_MessageReceived(string message)
        {
            if (DumpToDebug)
                Debug.WriteLine(message);

            if (IsPlaying)
                return;

            var ev = JsonApiEvent.Parse(message);

            if (!ev.Equals(default(JsonApiEvent)))
            {
                bool subscribed = Subscribers.Any(o => o.Key.IsMatch(ev.URI));

                var args = new MessageReceivedEventArgs(ev, message, subscribed);
                MessageReceived?.Invoke(this, args);

                if (args.Handled)
                    return;

                HandleEvent(ev);
            }
        }

        public void Close()
        {
            Socket.Close();
        }

        private static Regex BuildRegex(string path) => new Regex($"^{Regex.Escape(path)}$");

        public void Subscribe<T>(string path, MessageHandlerDelegate<T> action)
        {
            Subscribe(BuildRegex(path), action);
        }
        
        public void Subscribe<T>(Regex regex, MessageHandlerDelegate<T> action)
        {
            Subscribers.Add(regex, new Tuple<Type, Delegate>(typeof(T), action));
        }
        
        public void Unsubscribe(string path)
        {
            Unsubscribe(BuildRegex(path));
        }

        public void Unsubscribe(Regex regex)
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

        public void Playback(EventData[] events, float speed = 1, CancellationToken? cancelToken = null)
        {
            IsPlaying = true;

            new Thread(() =>
            {
                TimeSpan lastTime = events[0].TimeSinceStart; //Skip delay before first event

                foreach (var item in events)
                {
                    if (cancelToken?.IsCancellationRequested == true)
                        break;

                    Thread.Sleep((int)(item.TimeSinceStart.Subtract(lastTime).TotalMilliseconds / speed));
                    lastTime = item.TimeSinceStart;

                    Debug.WriteLine("{0} [{1}] {2}", item.TimeSinceStart, item.JsonEvent.EventType, item.JsonEvent.URI);

                    HandleEvent(item.JsonEvent);
                }

                IsPlaying = false;
            }).Start();
        }
        
        private void HandleEvent(JsonApiEvent @event)
        {
            foreach (var item in Subscribers.Where(o => o.Key.IsMatch(@event.URI)))
            {
                if (item.Value.Item1 == typeof(JsonApiEvent))
                {
                    item.Value.Item2.DynamicInvoke(@event);
                }
                else
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
}
