namespace LCU.NET.API_Models
{
    public class LolLobbyLobbyParticipantDto
    {
        public bool allowedChangeActivity { get; set; }
        public bool allowedInviteOthers { get; set; }
        public bool allowedKickOthers { get; set; }
        public bool allowedStartActivity { get; set; }
        public bool allowedToggleInvite { get; set; }
        public bool autoFillEligible { get; set; }
        public bool autoFillProtectedForPromos { get; set; }
        public bool autoFillProtectedForSoloing { get; set; }
        public bool autoFillProtectedForStreaking { get; set; }
        public int botChampionId { get; set; }
    
        /// <summary>
        /// Possible values: ['NONE', 'EASY', 'MEDIUM', 'HARD', 'UBER', 'TUTORIAL', 'INTRO']
        /// </summary>
        public string botDifficulty { get; set; }
    
        public string botId { get; set; }
        public string firstPositionPreference { get; set; }
        public bool isBot { get; set; }
        public bool isLeader { get; set; }
        public bool isSpectator { get; set; }
        public string lastSeasonHighestRank { get; set; }
        public string puuid { get; set; }
        public bool ready { get; set; }
        public string secondPositionPreference { get; set; }
        public bool showGhostedBanner { get; set; }
        public int summonerIconId { get; set; }
        public int summonerId { get; set; }
        public string summonerInternalName { get; set; }
        public int summonerLevel { get; set; }
        public string summonerName { get; set; }
        public int teamId { get; set; }
    }
}
