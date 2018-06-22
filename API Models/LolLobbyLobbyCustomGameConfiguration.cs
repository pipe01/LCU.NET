namespace LCU.NET.API_Models
{
    public class LolLobbyLobbyCustomGameConfiguration
    {
        public string gameMode { get; set; }
        public string gameMutator { get; set; }
        public string gameServerRegion { get; set; }
        public LolLobbyQueueGameTypeConfig gameTypeConfig { get; set; }
        public int mapId { get; set; }
        public int maxPlayerCount { get; set; }
        public LolLobbyQueueGameTypeConfig mutators { get; set; }
    
        /// <summary>
        /// Possible values: ['NotAllowed', 'LobbyAllowed', 'FriendsAllowed', 'AllAllowed']
        /// </summary>
        public string spectatorPolicy { get; set; }
    
        public int teamSize { get; set; }
        public string tournamentGameMode { get; set; }
        public string tournamentPassbackDataPacket { get; set; }
        public string tournamentPassbackUrl { get; set; }
    }
}
