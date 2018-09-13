using LCU.NET.API_Models;
using RestSharp;
using System.Threading.Tasks;
using static LCU.NET.LeagueClient;

namespace LCU.NET.Plugins.LoL
{
    public interface ILogin
    {
        Task DeleteSessionAsync();
        Task<LolLoginLoginSession> GetSessionAsync();
        Task<LolLoginLoginSession> PostSessionAsync(string username, string password);
    }

    public class Login : ILogin
    {
        public const string Endpoint = "/lol-login/v1/session";

        private ILeagueClient Client;
        internal Login(ILeagueClient client)
        {
            this.Client = client;
        }
        
        public Task DeleteSessionAsync()
            => Client.MakeRequestAsync(Endpoint, Method.DELETE);
        
        public Task<LolLoginLoginSession> GetSessionAsync()
            => Client.MakeRequestAsync<LolLoginLoginSession>(Endpoint, Method.GET);
        
        public Task<LolLoginLoginSession> PostSessionAsync(string username, string password)
            => Client.MakeRequestAsync<LolLoginLoginSession>(Endpoint, Method.POST, 
                new LolLoginUsernameAndPassword { username = username, password = password });
    }
}
