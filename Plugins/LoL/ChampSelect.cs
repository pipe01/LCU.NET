using LCU.NET.API_Models;
using RestSharp;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public static class ChampSelect
    {
        public const string Endpoint = "/lol-champ-select/v1/session";

        [APIMethod(Endpoint, Method.GET)]
        public static Task<LolChampSelectChampSelectSession> GetSessionAsync() => MakeRequestAsync<LolChampSelectChampSelectSession>();
        
        [APIMethod(Endpoint + "/my-selection", Method.PATCH)]
        public static Task PatchMySelectionAsync(LolChampSelectChampSelectMySelection selection) => MakeRequestAsync(selection);
        
        [APIMethod(Endpoint + "/timer", Method.GET)]
        public static Task<LolChampSelectChampSelectTimer> GetTimerAsync() => MakeRequestAsync<LolChampSelectChampSelectTimer>();
    }
}
