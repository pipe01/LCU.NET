namespace LCU.NET
{
    public class LolItemSetsItemSet
    {
        public int[] associatedChampions { get; set; }
        public int[] associatedMaps { get; set; }
        public LolItemSetsItemSetBlock[] blocks { get; set; }
        public string map { get; set; }
        public string mode { get; set; }
        public LolItemSetsPreferredItemSlot[] preferredItemSlots { get; set; }
        public int sortrank { get; set; }
        public string startedFrom { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string uid { get; set; }
    }
}
