namespace LCU.NET
{
    public class LolSummonerSummoner
    {
        public int accountId { get; set; }
        public string displayName { get; set; }
        public string internalName { get; set; }
        public string lastSeasonHighestRank { get; set; }
        public int percentCompleteForNextLevel { get; set; }
        public int profileIconId { get; set; }
        public string puuid { get; set; }
        public LolSummonerSummonerRerollPoints rerollPoints { get; set; }
        public int summonerId { get; set; }
        public int summonerLevel { get; set; }
        public int xpSinceLastLevel { get; set; }
        public int xpUntilNextLevel { get; set; }
    }
}
