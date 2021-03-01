using LCU.NET.WAMP;
using System;

namespace LCU.NET
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public bool Handled { get; set; }
        public bool HasSubscribers { get; }
        public JsonApiEvent Event { get; }
        public string RawData { get; }

        internal MessageReceivedEventArgs(JsonApiEvent @event, string raw, bool hasSubscribers)
        {
            this.HasSubscribers = hasSubscribers;
            this.Event = @event;
            this.RawData = raw;
        }
    }
}
