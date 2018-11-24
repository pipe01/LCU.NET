using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCU.NET.Plugins.LoL
{
    public interface ISummoner
    {
        Task<LolSummonerSummoner> GetCurrentSummoner();
        Task<LolSummonerSummoner> GetSummoner(string name);
        Task<LolSummonerSummoner> GetSummonerByID(long id);
    }

    public class Summoner : ISummoner
    {
        private ILeagueClient Client;
        public Summoner(ILeagueClient client)
        {
            this.Client = client;
        }

        public Task<LolSummonerSummoner> GetCurrentSummoner()
            => Client.MakeRequestAsync<LolSummonerSummoner>("/lol-summoner/v1/current-summoner", Method.GET);

        public Task<LolSummonerSummoner> GetSummoner(string name)
            => Client.MakeRequestAsync<LolSummonerSummoner>("/lol-summoner/v1/summoners", Method.GET, null,
                req => req.AddQueryParameter("name", name));

        public Task<LolSummonerSummoner> GetSummonerByID(long id)
            => Client.MakeRequestAsync<LolSummonerSummoner>($"/lol-summoner/v1/summoners/{id}", Method.GET);
    }
}
