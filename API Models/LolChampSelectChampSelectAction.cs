namespace LCU.NET.API_Models
{
    public class LolChampSelectChampSelectAction
    {
        public int actorCellId { get; set; }
        public int championId { get; set; }
        public bool completed { get; set; }
        public int id { get; set; }
        public int pickTurn { get; set; }
        public string type { get; set; }
    }
}