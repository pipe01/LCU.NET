using RestSharp;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins
{
    public static class RiotClient
    {
        public static void KillAndRestartUX()
            => MakeRequest("/riotclient/kill-and-restart-ux", Method.POST);

        public static void KillUX()
            => MakeRequest("/riotclient/kill-ux", Method.POST);

        public static void LaunchUX()
            => MakeRequest("/riotclient/launch-ux", Method.POST);
    }
}
