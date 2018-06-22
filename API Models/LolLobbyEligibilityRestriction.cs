namespace LCU.NET.API_Models
{
    public class LolLobbyEligibilityRestriction
    {
        public int expiredTimestamp { get; set; }
        public object restrictionArgs { get; set; }
    
        /// <summary>
        /// Possible values: ['QueueDisabled', 'QueueUnsupported', 'PlayerLevelRestriction', 'PlayerTimedRestriction', 'PlayerBannedRestriction', 'PlayerAvailableChampionRestriction', 'TeamDivisionRestriction', 'TeamMaxSizeRestriction', 'TeamMinSizeRestriction', 'PlayerBingeRestriction', 'PlayerDodgeRestriction', 'PlayerInGameRestriction', 'PlayerLeaverBustedRestriction', 'PlayerLeaverTaintedWarningRestriction', 'PlayerMaxLevelRestriction', 'PlayerMinLevelRestriction', 'PlayerMinorRestriction', 'PlayerRankedSuspensionRestriction', 'TeamHighMMRMaxSizeRestriction', 'TeamSizeRestriction', 'PrerequisiteQueuesNotPlayedRestriction', 'UnknownRestriction']
        /// </summary>
        public string restrictionCode { get; set; }
    
        public int[] summonerIds { get; set; }
    }
}
