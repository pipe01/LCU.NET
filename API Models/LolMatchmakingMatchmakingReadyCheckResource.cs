namespace LCU.NET
{
    public class LolMatchmakingMatchmakingReadyCheckResource
    {
        public int[] declinerIds { get; set; }
    
        /// <summary>
        /// Possible values: ['None', 'Warning', 'Penalty']
        /// </summary>
        public string dodgeWarning { get; set; }
    
    
        /// <summary>
        /// Possible values: ['None', 'Accepted', 'Declined']
        /// </summary>
        public string playerResponse { get; set; }
    
    
        /// <summary>
        /// Possible values: ['Invalid', 'InProgress', 'EveryoneReady', 'StrangerNotReady', 'PartyNotReady', 'Error']
        /// </summary>
        public string state { get; set; }
    
        public bool suppressUx { get; set; }
        public float timer { get; set; }
    }
}
