using LCU.NET.API_Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public static class Lobby
    {
        /// <summary>
        /// Adds a bot to a custom game lobby.
        /// </summary>
        public static void PostCustomBot(LolLobbyLobbyBotParams parameters)
            => MakeRequest("/lol-lobby/v1/lobby/custom/bots", Method.POST, parameters);
    }
}
