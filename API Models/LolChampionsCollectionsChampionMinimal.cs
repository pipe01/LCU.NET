namespace LCU.NET
{
    public class LolChampionsCollectionsChampionMinimal
    {
        public bool active { get; set; }
        public string alias { get; set; }
        public string banVoPath { get; set; }
        public bool botEnabled { get; set; }
        public string chooseVoPath { get; set; }
        public string[] disabledQueues { get; set; }
        public bool freeToPlay { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public LolChampionsCollectionsOwnership ownership { get; set; }
        public long purchased { get; set; }
        public bool rankedPlayEnabled { get; set; }
        public string[] roles { get; set; }
        public string squarePortraitPath { get; set; }
        public string stingerSfxPath { get; set; }
        public string title { get; set; }
    }
}
