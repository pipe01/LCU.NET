namespace LCU.NET
{
    public class LolMatchmakingMatchmakingDodgeData
    {
        public int dodgerId { get; set; }
    
        /// <summary>
        /// Possible values: ['Invalid', 'PartyDodged', 'StrangerDodged', 'TournamentDodged']
        /// </summary>
        public string state { get; set; }
    
    }
}
