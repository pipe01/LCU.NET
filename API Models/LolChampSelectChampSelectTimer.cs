namespace LCU.NET.API_Models
{
    public class LolChampSelectChampSelectTimer
    {
        public int adjustedTimeLeftInPhase { get; set; }
        public int adjustedTimeLeftInPhaseInSec { get; set; }
        public int internalNowInEpochMs { get; set; }
        public bool isInfinite { get; set; }
        public string phase { get; set; }
        public int timeLeftInPhase { get; set; }
        public int timeLeftInPhaseInSec { get; set; }
        public int totalTimeInPhase { get; set; }
    }
}
