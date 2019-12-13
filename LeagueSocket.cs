using LCU.NET.API_Models;
using LCU.NET.Utils;
using LCU.NET.WAMP;
using Newtonsoft.Json.Linq;
using Ninject;
using RestSharp;
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
        ILeagueClient Client { get; set; }

        bool Connect(int port, string password);
        void Close();

        void Subscribe<T>(object owner, string path, MessageHandlerDelegate<T> action);
        void Subscribe<T>(string path, MessageHandlerDelegate<T> action);
        Task SubscribeAndUpdate<T>(object owner, string path, MessageHandlerDelegate<T> action);
        Task SubscribeAndUpdate<T>(string path, MessageHandlerDelegate<T> action);
        void Subscribe<T>(object owner, Regex regex, MessageHandlerDelegate<T> action);
        void Subscribe<T>(Regex regex, MessageHandlerDelegate<T> action);

        void UnsubscribeAll(object withOwner);
        void Unsubscribe(string path);
        void Unsubscribe(Regex regex);

        void HandleEvent(JsonApiEvent @event);

        void Playback(EventData[] events, float speed = 1, CancellationToken? cancelToken = null);
    }

    public class LeagueSocket : ILeagueSocket
    {
        private struct Subscription
        {
            public object OwnerObject { get; }
            public Type ModelType { get; }
            public Delegate Callback { get; }

            public Subscription(object ownerObject, Type modelType, Delegate callback) : this()
            {
                this.OwnerObject = ownerObject;
                this.ModelType = modelType;
                this.Callback = callback;
            }

            public Subscription(Type modelType, Delegate callback) : this(null, modelType, callback)
            {
            }
        }

        private readonly ILSocket Socket;
        
        private readonly IDictionary<Regex, Subscription> Subscribers = new Dictionary<Regex, Subscription>();
        
        public ILeagueClient Client { get; set; }

        public bool DumpToDebug { get; set; }
        public bool IsPlaying { get; private set; }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event Action Closed;


        public LeagueSocket(ILSocket socket)
        {
            if (File.Exists("log.txt"))
                File.Delete("log.txt");

            this.Socket = socket;
            socket.Closed += () => Closed?.Invoke();
            socket.MessageReceived += Socket_MessageReceived;
        }

        public bool Connect(int port, string password)
        {
            var r = Socket.Init(port, password);

            if (r)
                Debug.WriteLine("WebSocket connected");

            return r;
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
                bool subscribed = Subscribers.ToArray().Any(o => o.Key.IsMatch(ev.URI));

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
            => Subscribe(null, path, action);

        public void Subscribe<T>(object owner, string path, MessageHandlerDelegate<T> action)
        {
            Subscribe(BuildRegex(path), action);
        }

        public Task SubscribeAndUpdate<T>(string path, MessageHandlerDelegate<T> action)
            => SubscribeAndUpdate(null, path, action);

        public async Task SubscribeAndUpdate<T>(object owner, string path, MessageHandlerDelegate<T> action)
        {
            Subscribe(owner, BuildRegex(path), action);

            if (!Client.IsConnected)
                return;

            T data = default;
            bool success = true;

            try
            {
                data = await Client.MakeRequestAsync<T>(path, Method.GET);
            }
            catch
            {
                success = false;
            }

            if (success)
                action(EventType.Update, data);
        }

        public void Subscribe<T>(Regex regex, MessageHandlerDelegate<T> action)
            => Subscribe(null, regex, action);

        public void Subscribe<T>(object owner, Regex regex, MessageHandlerDelegate<T> action)
        {
            Subscribers.Add(regex, new Subscription(owner, typeof(T), action));
        }
        
        public void UnsubscribeAll(object withOwner)
        {
            if (withOwner == null)
                throw new ArgumentNullException(nameof(withOwner));

            foreach (var item in Subscribers.Where(o => o.Value.OwnerObject == withOwner).ToArray())
            {
                Subscribers.Remove(item.Key);
            }
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

                    if (speed > 0)
                        Thread.Sleep((int)(item.TimeSinceStart.Subtract(lastTime).TotalMilliseconds / speed));

                    lastTime = item.TimeSinceStart;

                    Debug.WriteLine("{0} [{1}] {2}", item.TimeSinceStart, item.JsonEvent.EventType, item.JsonEvent.URI);

                    HandleEvent(item.JsonEvent);
                }

                IsPlaying = false;
            }).Start();
        }
        
        public void HandleEvent(JsonApiEvent @event)
        {
            foreach (var item in Subscribers.ToArray().Where(o => o.Key.IsMatch(@event.URI)))
            {
                if (item.Value.ModelType == typeof(JsonApiEvent))
                {
                    item.Value.Callback.DynamicInvoke(@event);
                }
                else
                {
                    object data;

                    try
                    {
                        data = @event.GetData(item.Value.ModelType);
                    }
                    catch (InvalidCastException)
                    {
                        //TODO Do something?
                        continue;
                    }

                    item.Value.Callback.DynamicInvoke(@event.EventType, data);
                }
            }
        }
    }
}
