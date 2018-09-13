using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public interface IMatchmaking
    {
        Task<LolMatchmakingMatchmakingReadyCheckResource> GetReadyCheck();
        Task PostReadyCheckAccept();
        Task PostReadyCheckDecline();
    }

    public class Matchmaking : IMatchmaking
    {
        public const string ReadyCheckEndpoint = "/lol-matchmaking/v1/ready-check";

        private ILeagueClient Client;
        public Matchmaking(ILeagueClient client)
        {
            this.Client = client;
        }

        public Task<LolMatchmakingMatchmakingReadyCheckResource> GetReadyCheck()
            => Client.MakeRequestAsync<LolMatchmakingMatchmakingReadyCheckResource>(ReadyCheckEndpoint, Method.GET);
        
        public Task PostReadyCheckAccept()
            => Client.MakeRequestAsync(ReadyCheckEndpoint + "/accept", Method.POST);
        
        public Task PostReadyCheckDecline()
            => Client.MakeRequestAsync(ReadyCheckEndpoint + "/decline", Method.POST);
    }
}
