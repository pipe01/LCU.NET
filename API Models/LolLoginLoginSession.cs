using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoL_API.API_Models
{
    public class LolLoginLoginSession
    {
        public int accountId { get; set; }
        public bool connected { get; set; }
        public LolLoginLoginError error { get; set; }
        public object gasToken { get; set; }
        public string idToken { get; set; }
        public bool isNewPlayer { get; set; }
        public string puuid { get; set; }
        public LolLoginLoginQueue queueStatus { get; set; }
        public string state { get; set; }
        public int summonerId { get; set; }
        public string userAuthToken { get; set; }
        public string username { get; set; }
    }
}
