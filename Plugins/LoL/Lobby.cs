using LCU.NET.API_Models;
using RestSharp;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public static class Lobby
    {
        /// <summary>
        /// Adds a bot to a custom game lobby.
        /// </summary>
        public static void PostCustomBot(LolLobbyLobbyBotParams parameters)
            => MakeRequest("/lol-lobby/v1/lobby/custom/bots", Method.POST, parameters);

        /// <summary>
        /// Gets the current lobby.
        /// </summary>
        public static LolLobbyLobbyDto GetLobby()
            => MakeRequest<LolLobbyLobbyDto>("/lol-lobby/v2/lobby", Method.GET);

        /// <summary>
        /// Creates a new lobby or changes the current one.
        /// </summary>
        /// <param name="lobbyChange">The lobby data.</param>
        public static LolLobbyLobbyDto PostLobby(LolLobbyLobbyChangeGameDto lobbyChange)
            => MakeRequest<LolLobbyLobbyDto>("/lol-lobby/v2/lobby", Method.POST, lobbyChange);

        /// <summary>
        /// Deletes (exits) the current lobby.
        /// </summary>
        public static void DeleteLobby()
            => MakeRequest("/lol-lobby/v2/lobby", Method.DELETE);
    }
}
