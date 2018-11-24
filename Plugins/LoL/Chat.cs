using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCU.NET.Plugins.LoL
{
    public interface IChat
    {
        Task<LolChatFriendResource[]> GetFriends();
        Task<LolChatFriendResource> GetFriendById(int id);
    }

    public class Chat : IChat
    {
        private ILeagueClient Client;
        public Chat(ILeagueClient client)
        {
            this.Client = client;
        }

        public Task<LolChatFriendResource> GetFriendById(int id)
            => Client.MakeRequestAsync<LolChatFriendResource>($"/lol-chat/v1/friends/{id}", Method.GET);

        public Task<LolChatFriendResource[]> GetFriends()
            => Client.MakeRequestAsync<LolChatFriendResource[]>("/lol-chat/v1/friends", Method.GET);
    }
}
