using LCU.NET.API_Models;
using RestSharp;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public interface ILobby
    {
        Task PostCustomBotAsync(LolLobbyLobbyBotParams parameters);
        Task<LolLobbyLobbyDto> GetLobbyAsync();
        Task<LolLobbyLobbyDto> PostLobbyAsync(LolLobbyLobbyChangeGameDto lobbyChange);
        Task DeleteLobbyAsync();
    }

    public class Lobby : ILobby
    {
        public const string Endpoint = "/lol-lobby/v2/lobby";

        private ILeagueClient Client;
        public Lobby(ILeagueClient client)
        {
            this.Client = client;
        }
        
        /// <summary>
        /// Adds a bot to a custom game lobby.
        /// </summary>
        public Task PostCustomBotAsync(LolLobbyLobbyBotParams parameters)
            => Client.MakeRequestAsync("/lol-lobby/v1/lobby/custom/bots", Method.POST, parameters);
        
        /// <summary>
        /// Gets the current lobby.
        /// </summary>
        public Task<LolLobbyLobbyDto> GetLobbyAsync()
            => Client.MakeRequestAsync<LolLobbyLobbyDto>(Endpoint, Method.GET);
        
        /// <summary>
        /// Creates a new lobby or changes the current one.
        /// </summary>
        /// <param name="lobbyChange">The lobby data.</param>
        public Task<LolLobbyLobbyDto> PostLobbyAsync(LolLobbyLobbyChangeGameDto lobbyChange)
            => Client.MakeRequestAsync<LolLobbyLobbyDto>(Endpoint, Method.POST, lobbyChange);
        
        /// <summary>
        /// Exits the current lobby.
        /// </summary>
        public Task DeleteLobbyAsync()
            => Client.MakeRequestAsync(Endpoint, Method.DELETE);
    }
}
