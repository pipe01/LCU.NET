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
        public static Task CreateBlindPickLobbyAsync()
            => Lobby.PostLobbyAsync(new LolLobbyLobbyChangeGameDto { queueId = 430 });

        public static Task CreateDraftPickLobbyAsync()
            => Lobby.PostLobbyAsync(new LolLobbyLobbyChangeGameDto { queueId = 400 });
    }
}
