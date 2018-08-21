using LCU.NET.API_Models;
using RestSharp;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public static class ChampSelect
    {
        public const string Endpoint = "/lol-champ-select/v1";

        [APIMethod(Endpoint + "/session", Method.GET)]
        public static Task<LolChampSelectChampSelectSession> GetSessionAsync() => MakeRequestAsync<LolChampSelectChampSelectSession>();
        
        [APIMethod(Endpoint + "/session/my-selection", Method.PATCH)]
        public static Task PatchMySelectionAsync(LolChampSelectChampSelectMySelection selection) => MakeRequestAsync(selection);
        
        [APIMethod(Endpoint + "/session/timer", Method.GET)]
        public static Task<LolChampSelectChampSelectTimer> GetTimerAsync() => MakeRequestAsync<LolChampSelectChampSelectTimer>();
    }
}
