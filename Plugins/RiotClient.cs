using LCU.NET.API_Models;
using RestSharp;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins
{
    public static class RiotClient
    {
        [APIMethod("/riotclient/kill-and-restart-ux", Method.POST)]
        public static Task KillAndRestartUXAsync()
            => MakeRequestAsync();

        [APIMethod("/riotclient/kill-ux", Method.POST)]
        public static Task KillUXAsync()
            => MakeRequestAsync();

        [APIMethod("/riotclient/launch-ux", Method.POST)]
        public static Task LaunchUXAsync()
            => MakeRequestAsync();

        [APIMethod("/riotclient/region-locale", Method.GET)]
        public static Task<RegionLocale> GetRegionLocale()
            => MakeRequestAsync<RegionLocale>();
    }
}
