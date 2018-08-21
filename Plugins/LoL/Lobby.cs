using LCU.NET.API_Models;
using RestSharp;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public static class Lobby
    {
        public const string Endpoint = "/lol-lobby/v2/lobby";

        /// <summary>
        /// Adds a bot to a custom game lobby.
        /// </summary>
        [APIMethod("/lol-lobby/v1/lobby/custom/bots", Method.POST)]
        public static Task PostCustomBotAsync(LolLobbyLobbyBotParams parameters)
            => MakeRequestAsync(parameters);
        
        /// <summary>
        /// Gets the current lobby.
        /// </summary>
        [APIMethod(Endpoint, Method.GET)]
        public static Task<LolLobbyLobbyDto> GetLobbyAsync()
            => MakeRequestAsync<LolLobbyLobbyDto>();
        
        /// <summary>
        /// Creates a new lobby or changes the current one.
        /// </summary>
        /// <param name="lobbyChange">The lobby data.</param>
        [APIMethod(Endpoint, Method.POST)]
        public static Task<LolLobbyLobbyDto> PostLobbyAsync(LolLobbyLobbyChangeGameDto lobbyChange)
            => MakeRequestAsync<LolLobbyLobbyDto>(lobbyChange);
        
        /// <summary>
        /// Exits the current lobby.
        /// </summary>
        [APIMethod(Endpoint, Method.DELETE)]
        public static Task DeleteLobbyAsync()
            => MakeRequestAsync();
    }
}
