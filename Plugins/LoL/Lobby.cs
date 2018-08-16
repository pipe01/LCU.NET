using LCU.NET.API_Models;
using RestSharp;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public static class Lobby
    {
        /// <summary>
        /// Adds a bot to a custom game lobby.
        /// </summary>
        [APIMethod("/lol-lobby/v1/lobby/custom/bots", Method.POST)]
        public static void PostCustomBot(LolLobbyLobbyBotParams parameters)
            => MakeRequest(parameters);

        /// <summary>
        /// Adds a bot to a custom game lobby.
        /// </summary>
        [APIMethod("/lol-lobby/v1/lobby/custom/bots", Method.POST)]
        public static Task PostCustomBotAsync(LolLobbyLobbyBotParams parameters)
            => MakeRequestAsync(parameters);

        /// <summary>
        /// Gets the current lobby.
        /// </summary>
        [APIMethod("/lol-lobby/v2/lobby", Method.GET)]
        public static LolLobbyLobbyDto GetLobby()
            => MakeRequest<LolLobbyLobbyDto>();

        /// <summary>
        /// Gets the current lobby.
        /// </summary>
        [APIMethod("/lol-lobby/v2/lobby", Method.GET)]
        public static Task<LolLobbyLobbyDto> GetLobbyAsync()
            => MakeRequestAsync<LolLobbyLobbyDto>();

        /// <summary>
        /// Creates a new lobby or changes the current one.
        /// </summary>
        /// <param name="lobbyChange">The lobby data.</param>
        [APIMethod("/lol-lobby/v2/lobby", Method.POST)]
        public static LolLobbyLobbyDto PostLobby(LolLobbyLobbyChangeGameDto lobbyChange)
            => MakeRequest<LolLobbyLobbyDto>(lobbyChange);

        /// <summary>
        /// Creates a new lobby or changes the current one.
        /// </summary>
        /// <param name="lobbyChange">The lobby data.</param>
        [APIMethod("/lol-lobby/v2/lobby", Method.POST)]
        public static Task<LolLobbyLobbyDto> PostLobbyAsync(LolLobbyLobbyChangeGameDto lobbyChange)
            => MakeRequestAsync<LolLobbyLobbyDto>(lobbyChange);

        /// <summary>
        /// Deletes (exits) the current lobby.
        /// </summary>
        [APIMethod("/lol-lobby/v2/lobby", Method.DELETE)]
        public static void DeleteLobby()
            => MakeRequest();

        /// <summary>
        /// Exits the current lobby.
        /// </summary>
        [APIMethod("/lol-lobby/v2/lobby", Method.DELETE)]
        public static Task DeleteLobbyAsync()
            => MakeRequestAsync();
    }
}
