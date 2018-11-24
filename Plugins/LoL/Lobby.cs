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
        Task<LolLobbyLobbyParticipantDto[]> GetMembers();
        Task<LolLobbyLobbyInvitationDto[]> GetInvitations();
        Task<LolLobbyLobbyInvitationDto[]> PostInvitations(LolLobbyLobbyInvitationDto[] invitations);
        Task PostMatchmakingSearch();
        Task DeleteMatchmakingSearch();
        Task<LolLobbyLobbyMatchmakingSearchResource> GetSearchState();
    }

    public class Lobby : ILobby
    {
        public const string Endpoint = "/lol-lobby/v2/lobby";
        public const string MembersEndpoint = Endpoint + "/members";
        public const string InvitationsEndpoint = Endpoint + "/invitations";
        public const string MatchmakingEndpoint = Endpoint + "/matchmaking";

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

        /// <summary>
        /// Gets the members of the current lobby.
        /// </summary>
        public Task<LolLobbyLobbyParticipantDto[]> GetMembers()
            => Client.MakeRequestAsync<LolLobbyLobbyParticipantDto[]>(MembersEndpoint, Method.GET);

        /// <summary>
        /// Gets the invitations of the current lobby.
        /// </summary>
        public Task<LolLobbyLobbyInvitationDto[]> GetInvitations()
            => Client.MakeRequestAsync<LolLobbyLobbyInvitationDto[]>(InvitationsEndpoint, Method.GET);

        /// <summary>
        /// Sends one or more invitations.
        /// </summary>
        /// <param name="invitations">The invitations to send</param>
        public Task<LolLobbyLobbyInvitationDto[]> PostInvitations(LolLobbyLobbyInvitationDto[] invitations)
            => Client.MakeRequestAsync<LolLobbyLobbyInvitationDto[]>(InvitationsEndpoint, Method.POST, invitations);

        public Task PostMatchmakingSearch()
            => Client.MakeRequestAsync(MatchmakingEndpoint + "/search", Method.POST);

        public Task DeleteMatchmakingSearch()
            => Client.MakeRequestAsync(MatchmakingEndpoint + "/search", Method.DELETE);

        public Task<LolLobbyLobbyMatchmakingSearchResource> GetSearchState()
            => Client.MakeRequestAsync<LolLobbyLobbyMatchmakingSearchResource>(MatchmakingEndpoint + "/search-state", Method.GET);
    }
}
