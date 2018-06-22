namespace LCU.NET.API_Models
{
    public class LolLobbyLobbyChangeGameDto
    {
        public LolLobbyLobbyCustomGameLobby customGameLobby { get; set; }
        public object gameCustomization { get; set; }
        public bool isCustom { get; set; }
        public int queueId { get; set; }
    }
}
