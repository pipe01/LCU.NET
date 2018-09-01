using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public static class Champions
    {
        [APIMethod("/lol-champions/v1/owned-champions-minimal", Method.GET)]
        public static Task<LolChampionsCollectionsChampionMinimal[]> GetOwnedChampionsMinimal()
            => MakeRequestAsync<LolChampionsCollectionsChampionMinimal[]>();
    }
}
