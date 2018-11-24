using System.Collections.Generic;

namespace LCU.NET
{
    public class LolMapsMaps
    {
        public Dictionary<string, string> assets { get; set; }
        public string description { get; set; }
        public string gameMode { get; set; }
        public string gameModeDescription { get; set; }
        public string gameModeName { get; set; }
        public string gameModeShortName { get; set; }
        public string gameMutator { get; set; }
        public int id { get; set; }
        public bool isDefault { get; set; }
        public bool isRGM { get; set; }
        public string mapStringId { get; set; }
        public string name { get; set; }
        public string platformId { get; set; }
        public string platformName { get; set; }
        public Dictionary<string, object> properties { get; set; }
        public LolMapsTutorialCard[] tutorialCards { get; set; }
    }
}
