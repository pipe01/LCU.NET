using RestSharp;
using System.Threading.Tasks;

namespace LCU.NET.Plugins.LoL
{
    public interface IChampions
    {
        Task<LolChampionsCollectionsChampionMinimal[]> GetOwnedChampionsMinimal();
    }

    public class Champions : IChampions
    {
        private readonly ILeagueClient Client;
        public Champions(ILeagueClient client)
        {
            this.Client = client;
        }
        
        public Task<LolChampionsCollectionsChampionMinimal[]> GetOwnedChampionsMinimal()
            => Client.MakeRequestAsync<LolChampionsCollectionsChampionMinimal[]>("/lol-champions/v1/owned-champions-minimal", Method.GET);
    }
}
