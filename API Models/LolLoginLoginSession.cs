namespace LCU.NET.API_Models
{
    public class LolLoginLoginSession
    {
        public long accountId { get; set; }
        public bool connected { get; set; }
        public LolLoginLoginError error { get; set; }
        public object gasToken { get; set; }
        public string idToken { get; set; }
        public bool isNewPlayer { get; set; }
        public string puuid { get; set; }
        public LolLoginLoginQueue queueStatus { get; set; }
        public string state { get; set; }
        public int summonerId { get; set; }
        public string userAuthToken { get; set; }
        public string username { get; set; }
    }
}
