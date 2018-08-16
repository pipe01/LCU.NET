using LCU.NET.API_Models;
using RestSharp;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public static class ChampSelect
    {
        [APIMethod("/lol-champ-select/v1/session", Method.GET)]
        public static LolChampSelectChampSelectSession GetSession() => MakeRequest<LolChampSelectChampSelectSession>();

        [APIMethod("/lol-champ-select/v1/session", Method.GET)]
        public static Task<LolChampSelectChampSelectSession> GetSessionAsync() => MakeRequestAsync<LolChampSelectChampSelectSession>();

        [APIMethod("/lol-champ-select/v1/session/my-selection", Method.PATCH)]
        public static void PatchMySelection(LolChampSelectChampSelectMySelection selection) => MakeRequest(selection);

        [APIMethod("/lol-champ-select/v1/session/my-selection", Method.PATCH)]
        public static Task PatchMySelectionAsync(LolChampSelectChampSelectMySelection selection) => MakeRequestAsync(selection);

        [APIMethod("/lol-champ-select/v1/session/timer", Method.GET)]
        public static LolChampSelectChampSelectTimer GetTimer() => MakeRequest<LolChampSelectChampSelectTimer>();
        
        [APIMethod("/lol-champ-select/v1/session/timer", Method.GET)]
        public static Task<LolChampSelectChampSelectTimer> GetTimerAsync() => MakeRequestAsync<LolChampSelectChampSelectTimer>();
    }
}
