using LCU.NET.API_Models;
using RestSharp;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public static class Login
    {
        public static void DeleteSession()
            => MakeRequest("/lol-login/v1/session", Method.DELETE);

        public static LolLoginLoginSession GetSession()
            => MakeRequest<LolLoginLoginSession>("/lol-login/v1/session", Method.GET);

        public static LolLoginLoginSession PostSession(string username, string password)
            => MakeRequest<LolLoginLoginSession>("/lol-login/v1/session", Method.POST, new LolLoginUsernameAndPassword { username = username, password = password });
    }
}
