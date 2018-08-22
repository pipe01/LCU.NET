using LCU.NET.API_Models;
using RestSharp;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public static class Login
    {
        public const string Endpoint = "/lol-login/v1/session";

        [APIMethod(Endpoint, Method.DELETE)]
        public static Task DeleteSessionAsync()
            => MakeRequestAsync();
        
        [APIMethod(Endpoint, Method.GET)]
        public static Task<LolLoginLoginSession> GetSessionAsync()
            => MakeRequestAsync<LolLoginLoginSession>();
        
        [APIMethod(Endpoint, Method.POST)]
        public static Task<LolLoginLoginSession> PostSessionAsync(string username, string password)
            => MakeRequestAsync<LolLoginLoginSession>(new LolLoginUsernameAndPassword { username = username, password = password });
    }
}
