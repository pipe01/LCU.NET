using RestSharp;
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
        private readonly ILeagueClient Client;
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
