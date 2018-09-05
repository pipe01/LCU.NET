using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public static class ItemSets
    {
        [APIMethod("/lol-item-sets/v1/item-sets/{summonerId}/sets", Method.GET)]
        public static Task<LolItemSetsItemSets> GetItemSets(int summonerId)
            => MakeRequestAsync<LolItemSetsItemSets>(args: summonerId.ToString());

        [APIMethod("/lol-item-sets/v1/item-sets/{summonerId}/sets", Method.POST)]
        public static Task PostItemSet(int summonerId, LolItemSetsItemSet itemSet)
            => MakeRequestAsync(itemSet, args: summonerId.ToString());

        [APIMethod("/lol-item-sets/v1/item-sets/{summonerId}/sets", Method.PUT)]
        public static Task PutItemSets(int summonerId, LolItemSetsItemSets itemSets)
            => MakeRequestAsync(itemSets, args: summonerId.ToString());
    }
}
