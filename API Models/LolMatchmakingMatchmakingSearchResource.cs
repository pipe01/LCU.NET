namespace LCU.NET
{
    public class LolMatchmakingMatchmakingSearchResource
    {
        public LolMatchmakingMatchmakingDodgeData dodgeData { get; set; }
        public LolMatchmakingMatchmakingSearchErrorResource[] errors { get; set; }
        public float estimatedQueueTime { get; set; }
        public bool isCurrentlyInQueue { get; set; }
        public string lobbyId { get; set; }
        public LolMatchmakingMatchmakingLowPriorityData lowPriorityData { get; set; }
        public int queueId { get; set; }
        public LolMatchmakingMatchmakingReadyCheckResource readyCheck { get; set; }
    
        /// <summary>
        /// Possible values: ['Invalid', 'AbandonedLowPriorityQueue', 'Canceled', 'Searching', 'Found', 'Error', 'ServiceError', 'ServiceShutdown']
        /// </summary>
        public string searchState { get; set; }
    
        public float timeInQueue { get; set; }
    }
}
