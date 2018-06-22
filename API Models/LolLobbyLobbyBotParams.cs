namespace LCU.NET.API_Models
{
    public class LolLobbyLobbyBotParams
    {
        /// <summary>
        /// ['NONE', 'EASY', 'MEDIUM', 'HARD', 'UBER', 'TUTORIAL', 'INTRO']
        /// </summary>
        public string botDifficulty { get; set; }

        public int championId { get; set; }
        public string teamId { get; set; }
    }
}
