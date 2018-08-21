using RestSharp;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins
{
    public static class RiotClient
    {
        public static Task KillAndRestartUXAsync()
            => MakeRequestAsync("/riotclient/kill-and-restart-ux", Method.POST);

        public static Task KillUXAsync()
            => MakeRequestAsync("/riotclient/kill-ux", Method.POST);

        public static Task LaunchUXAsync()
            => MakeRequestAsync("/riotclient/launch-ux", Method.POST);
    }
}
