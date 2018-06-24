using LCU.NET.API_Models;
using RestSharp;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public static class Login
    {
        [APIMethod("/lol-login/v1/session", Method.DELETE)]
        public static void DeleteSession()
            => MakeRequest();

        [APIMethod("/lol-login/v1/session", Method.GET)]
        public static LolLoginLoginSession GetSession()
            => MakeRequest<LolLoginLoginSession>();

        [APIMethod("/lol-login/v1/session", Method.POST)]
        public static LolLoginLoginSession PostSession(string username, string password)
            => MakeRequest<LolLoginLoginSession>(new LolLoginUsernameAndPassword { username = username, password = password });
    }
}
