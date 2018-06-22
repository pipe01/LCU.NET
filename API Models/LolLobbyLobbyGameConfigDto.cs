namespace LCU.NET.API_Models
{
    public class LolLobbyLobbyGameConfigDto
    {
        public int[] allowablePremadeSizes { get; set; }
        public string customLobbyName { get; set; }
        public string customMutatorName { get; set; }
        public string[] customRewardsDisabledReasons { get; set; }
    
        /// <summary>
        /// Possible values: ['NotAllowed', 'LobbyAllowed', 'FriendsAllowed', 'AllAllowed']
        /// </summary>
        public string customSpectatorPolicy { get; set; }
    
        public LolLobbyLobbyParticipantDto[] customSpectators { get; set; }
        public LolLobbyLobbyParticipantDto[] customTeam100 { get; set; }
        public LolLobbyLobbyParticipantDto[] customTeam200 { get; set; }
        public string gameMode { get; set; }
        public string gameMutator { get; set; }
        public bool isCustom { get; set; }
        public bool isLobbyFull { get; set; }
        public bool isTeamBuilderManaged { get; set; }
        public int mapId { get; set; }
        public int maxHumanPlayers { get; set; }
        public int maxLobbySize { get; set; }
        public int maxTeamSize { get; set; }
        public string pickType { get; set; }
        public bool premadeSizeAllowed { get; set; }
        public int queueId { get; set; }
        public bool showPositionSelector { get; set; }
    }
}
