namespace LCU.NET
{
    public class LolLobbyLobbyInvitationDto
    {
        public string invitationId { get; set; }
    
        /// <summary>
        /// Possible values: ['Requested', 'Pending', 'Accepted', 'Joined', 'Declined', 'Kicked', 'OnHold', 'Error']
        /// </summary>
        public string state { get; set; }
    
        public string timestamp { get; set; }
        public int toSummonerId { get; set; }
        public string toSummonerName { get; set; }
    }
}
