using LCU.NET.API_Models;
using RestSharp;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public static class Login
    {
        [APIMethod("/lol-login/v1/session", Method.DELETE)]
        public static Task DeleteSessionAsync()
            => MakeRequestAsync();
        
        [APIMethod("/lol-login/v1/session", Method.GET)]
        public static Task<LolLoginLoginSession> GetSessionAsync()
            => MakeRequestAsync<LolLoginLoginSession>();
        
        [APIMethod("/lol-login/v1/session", Method.POST)]
        public static Task<LolLoginLoginSession> PostSessionAsync(string username, string password)
            => MakeRequestAsync<LolLoginLoginSession>(new LolLoginUsernameAndPassword { username = username, password = password });
    }
}
