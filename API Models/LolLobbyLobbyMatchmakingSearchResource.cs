namespace LCU.NET
{
    public class LolLobbyLobbyMatchmakingSearchResource
    {
        public LolLobbyLobbyMatchmakingSearchErrorResource[] errors { get; set; }
        public LolLobbyLobbyMatchmakingLowPriorityDataResource lowPriorityData { get; set; }
    
        /// <summary>
        /// Possible values: ['Invalid', 'AbandonedLowPriorityQueue', 'Canceled', 'Searching', 'Found', 'Error', 'ServiceError', 'ServiceShutdown']
        /// </summary>
        public string searchState { get; set; }
    
    }
}
