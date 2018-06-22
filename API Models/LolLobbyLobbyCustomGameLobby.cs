namespace LCU.NET.API_Models
{
    public class LolLobbyLobbyCustomGameLobby
    {
        public LolLobbyLobbyCustomGameConfiguration configuration { get; set; }
        public int gameId { get; set; }
        public string lobbyName { get; set; }
        public string lobbyPassword { get; set; }
        public string[] practiceGameRewardsDisabledReasons { get; set; }
        public LolLobbyLobbyMember[] spectators { get; set; }
        public LolLobbyLobbyMember[] teamOne { get; set; }
        public LolLobbyLobbyMember[] teamTwo { get; set; }
    }
}
