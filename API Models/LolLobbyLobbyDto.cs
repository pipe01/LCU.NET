namespace LCU.NET.API_Models
{
    public class LolLobbyLobbyDto
    {
        public bool canStartActivity { get; set; }
        public string chatRoomId { get; set; }
        public string chatRoomKey { get; set; }
        public LolLobbyLobbyGameConfigDto gameConfig { get; set; }
        public LolLobbyLobbyInvitationDto[] invitations { get; set; }
        public LolLobbyLobbyParticipantDto localMember { get; set; }
        public LolLobbyLobbyParticipantDto[] members { get; set; }
        public string partyId { get; set; }
        public string partyType { get; set; }
        public LolLobbyEligibilityRestriction[] restrictions { get; set; }
    }
}
