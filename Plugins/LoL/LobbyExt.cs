using LCU.NET.API_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCU.NET.Plugins.LoL
{
    public static class LobbyExt
    {
        public static void CreateBlindPickLobby()
            => Lobby.PostLobby(new LolLobbyLobbyChangeGameDto { queueId = 430 });

        public static void CreateDraftPickLobby()
            => Lobby.PostLobby(new LolLobbyLobbyChangeGameDto { queueId = 400 });
    }
}
