using RestSharp;
using System.Threading.Tasks;

namespace LCU.NET.Plugins.LoL
{
    public interface IMaps
    {
        Task<LolMapsMaps[]> GetMaps();
        Task<LolMapsMaps> GetMapById(int id);
    }

    public class Maps : IMaps
    {
        private readonly ILeagueClient Client;
        public Maps(ILeagueClient client)
        {
            this.Client = client;
        }

        public Task<LolMapsMaps[]> GetMaps()
            => LeagueClient.Cache(() => Client.MakeRequestAsync<LolMapsMaps[]>("/lol-maps/v2/maps", Method.GET));

        public Task<LolMapsMaps> GetMapById(int id)
            => LeagueClient.Cache(id.ToString(), () => Client.MakeRequestAsync<LolMapsMaps>($"/lol-maps/v1/map/{id}", Method.GET));
    }
}
