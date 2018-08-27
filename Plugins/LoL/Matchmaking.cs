using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public static class Matchmaking
    {
        public const string ReadyCheckEndpoint = "/lol-matchmaking/v1/ready-check";

        [APIMethod(ReadyCheckEndpoint, Method.GET)]
        public static Task<LolMatchmakingMatchmakingReadyCheckResource> GetReadyCheck()
            => MakeRequestAsync<LolMatchmakingMatchmakingReadyCheckResource>();

        [APIMethod(ReadyCheckEndpoint + "/accept", Method.POST)]
        public static Task PostReadyCheckAccept()
            => MakeRequestAsync();

        [APIMethod(ReadyCheckEndpoint + "/decline", Method.POST)]
        public static Task PostReadyCheckDecline()
            => MakeRequestAsync();
    }
}
