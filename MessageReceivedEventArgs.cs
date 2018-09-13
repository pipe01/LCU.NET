using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCU.NET
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public bool Handled { get; set; }
        public bool HasSubscribers { get; }

        internal MessageReceivedEventArgs(bool hasSubscribers)
        {
            this.HasSubscribers = hasSubscribers;
        }
    }
}
