using RestSharp;
using System.Threading.Tasks;

namespace LCU.NET.Plugins.LoL
{
    public interface IItemsSets
    {
        Task<LolItemSetsItemSets> GetItemSets(int summonerId);
        Task PostItemSet(int summonerId, LolItemSetsItemSet itemSet);
        Task PutItemSets(int summonerId, LolItemSetsItemSets itemSets);
    }

    public class ItemSets : IItemsSets
    {
        private readonly ILeagueClient Client;
        public ItemSets(ILeagueClient client)
        {
            this.Client = client;
        }
        
        public Task<LolItemSetsItemSets> GetItemSets(int summonerId)
            => Client.MakeRequestAsync<LolItemSetsItemSets>($"/lol-item-sets/v1/item-sets/{summonerId}/sets", Method.GET);
        
        public Task PostItemSet(int summonerId, LolItemSetsItemSet itemSet)
            => Client.MakeRequestAsync($"/lol-item-sets/v1/item-sets/{summonerId}/sets", Method.POST);
        
        public Task PutItemSets(int summonerId, LolItemSetsItemSets itemSets)
            => Client.MakeRequestAsync($"/lol-item-sets/v1/item-sets/{summonerId}/sets", Method.PUT, itemSets);
    }
}
