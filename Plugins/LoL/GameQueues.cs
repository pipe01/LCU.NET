using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCU.NET.Plugins.LoL
{
    public interface IGameQueues
    {
        Task<LolGameQueuesQueue[]> GetQueues();
        Task<LolGameQueuesQueue> GetQueueById(int id);
    }

    public class GameQueues : IGameQueues
    {
        private readonly ILeagueClient Client;
        public GameQueues(ILeagueClient client)
        {
            this.Client = client;
        }

        public Task<LolGameQueuesQueue[]> GetQueues()
            => LeagueClient.Cache(
                () => Client.MakeRequestAsync<LolGameQueuesQueue[]>("/lol-game-queues/v1/queues", Method.GET));

        public Task<LolGameQueuesQueue> GetQueueById(int id)
            => LeagueClient.Cache(id.ToString(), 
                () => Client.MakeRequestAsync<LolGameQueuesQueue>($"/lol-game-queues/v1/queues/{id}", Method.GET));
    }
}
