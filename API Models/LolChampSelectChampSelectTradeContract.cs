namespace LCU.NET.API_Models
{
    public class LolChampSelectChampSelectTradeContract
    {
        public int cellId { get; set; }
        public int id { get; set; }
    
        /// <summary>
        /// Possible values: ['AVAILABLE', 'BUSY', 'INVALID', 'RECEIVED', 'SENT']
        /// </summary>
        public string state { get; set; }
    
    }
}
