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
        Task DeleteSearch();
        Task<LolMatchmakingMatchmakingSearchResource> GetSearch();
        Task PostSearch();
    }

    public class Matchmaking : IMatchmaking
    {
        public const string ReadyCheckEndpoint = "/lol-matchmaking/v1/ready-check";
        public const string SearchEndpoint = "/lol-matchmaking/v1/search";

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

        /// <summary>
        /// Cancels matchmaking search.
        /// </summary>
        public Task DeleteSearch()
            => Client.MakeRequestAsync(SearchEndpoint, Method.DELETE);

        public Task<LolMatchmakingMatchmakingSearchResource> GetSearch()
            => Client.MakeRequestAsync<LolMatchmakingMatchmakingSearchResource>(SearchEndpoint, Method.GET);

        /// <summary>
        /// Begins matchmaking search.
        /// </summary>
        public Task PostSearch()
            => Client.MakeRequestAsync(SearchEndpoint, Method.POST);
    }
}
