namespace LCU.NET.API_Models
{
    public class LolLobbyLobbyMember
    {
        public bool autoFillEligible { get; set; }
        public bool autoFillProtectedForPromos { get; set; }
        public bool autoFillProtectedForSoloing { get; set; }
        public bool autoFillProtectedForStreaking { get; set; }
        public int botChampionId { get; set; }
    
        /// <summary>
        /// Possible values: ['NONE', 'EASY', 'MEDIUM', 'HARD', 'UBER', 'TUTORIAL', 'INTRO']
        /// </summary>
        public string botDifficulty { get; set; }
    
        public bool canInviteOthers { get; set; }
        public string excludedPositionPreference { get; set; }
        public int id { get; set; }
        public bool isBot { get; set; }
        public bool isOwner { get; set; }
        public bool isSpectator { get; set; }
        public LolLobbyLobbyPositionPreferences positionPreferences { get; set; }
        public bool showPositionExcluder { get; set; }
        public string summonerInternalName { get; set; }
    }
}
