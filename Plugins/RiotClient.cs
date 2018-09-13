using LCU.NET.API_Models;
using RestSharp;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins
{
    public interface IRiotClient
    {
        Task KillAndRestartUXAsync();
        Task KillUXAsync();
        Task LaunchUXAsync();
        Task<RegionLocale> GetRegionLocale();
    }

    public class RiotClient : IRiotClient
    {
        private ILeagueClient Client;
        public RiotClient(ILeagueClient client)
        {
            this.Client = client;
        }
        
        public Task KillAndRestartUXAsync()
            => Client.MakeRequestAsync("/riotclient/kill-and-restart-ux", Method.POST);
        
        public Task KillUXAsync()
            => Client.MakeRequestAsync("/riotclient/kill-ux", Method.POST);
        
        public Task LaunchUXAsync()
            => Client.MakeRequestAsync("/riotclient/launch-ux", Method.POST);
        
        public Task<RegionLocale> GetRegionLocale()
            => Client.MakeRequestAsync<RegionLocale>("/riotclient/region-locale", Method.GET);
    }
}
