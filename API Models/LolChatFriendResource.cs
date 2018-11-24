using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCU.NET
{
    public class LolChatFriendResource
    {
        public string availability { get; set; }
        public int displayGroupId { get; set; }
        public int groupId { get; set; }
        public int icon { get; set; }
        public int id { get; set; }
        public bool isP2PConversationMuted { get; set; }
        public string lastSeenOnlineTimestamp { get; set; }
        public object lol { get; set; }
        public string name { get; set; }
        public string note { get; set; }
        public string statusMessage { get; set; }
    }
}
