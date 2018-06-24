namespace LCU.NET.API_Models
{
    public class LolChampSelectChampSelectSession
    {
        public LolChampSelectChampSelectAction[] actions { get; set; }
        public bool allowBattleBoost { get; set; }
        public bool allowDuplicatePicks { get; set; }
        public bool allowRerolling { get; set; }
        public bool allowSkinSelection { get; set; }
        public LolChampSelectChampSelectBannedChampions bans { get; set; }
        public int[] benchChampionIds { get; set; }
        public bool benchEnabled { get; set; }
        public int boostableSkinCount { get; set; }
        public LolChampSelectChampSelectChatRoomDetails chatDetails { get; set; }
        public bool isSpectating { get; set; }
        public int localPlayerCellId { get; set; }
        public LolChampSelectChampSelectPlayerSelection[] myTeam { get; set; }
        public int rerollsRemaining { get; set; }
        public LolChampSelectChampSelectPlayerSelection[] theirTeam { get; set; }
        public LolChampSelectChampSelectTimer timer { get; set; }
        public LolChampSelectChampSelectTradeContract[] trades { get; set; }
    }
}
