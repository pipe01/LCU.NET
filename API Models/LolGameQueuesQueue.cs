namespace LCU.NET
{
    public class LolGameQueuesQueue
    {
        public int[] allowablePremadeSizes { get; set; }
        public bool areFreeChampionsAllowed { get; set; }
        public string assetMutator { get; set; }
    
        /// <summary>
        /// Possible values: ['None', 'Custom', 'PvP', 'VersusAi', 'Alpha']
        /// </summary>
        public string category { get; set; }
    
        public int championsRequiredToPlay { get; set; }
        public string description { get; set; }
        public string detailedDescription { get; set; }
        public string gameMode { get; set; }
        public string gameMutator { get; set; }
        public LolGameQueuesQueueGameTypeConfig gameTypeConfig { get; set; }
        public int id { get; set; }
        public bool isRanked { get; set; }
        public bool isTeamBuilderManaged { get; set; }
        public bool isTeamOnly { get; set; }
        public long lastToggledOffTime { get; set; }
        public long lastToggledOnTime { get; set; }
        public int mapId { get; set; }
        public int maxLevel { get; set; }
        public int maxSummonerLevelForFirstWinOfTheDay { get; set; }
        public int maximumParticipantListSize { get; set; }
        public int minLevel { get; set; }
        public int minimumParticipantListSize { get; set; }
        public string name { get; set; }
        public int numPlayersPerTeam { get; set; }
    
        /// <summary>
        /// Possible values: ['Available', 'PlatformDisabled', 'DoesntMeetRequirements']
        /// </summary>
        public string queueAvailability { get; set; }
    
        public LolGameQueuesQueueReward queueRewards { get; set; }
        public string shortName { get; set; }
        public bool showPositionSelector { get; set; }
        public bool spectatorEnabled { get; set; }
        public string type { get; set; }
    }
}
