using RestSharp;
using System.Threading.Tasks;

namespace LCU.NET.Plugins
{
    public interface IProcessControl
    {
        Task Quit();
        Task Restart();
    }

    public class ProcessControl : IProcessControl
    {
        private readonly ILeagueClient Client;
        public ProcessControl(ILeagueClient client)
        {
            this.Client = client;
        }

        public Task Quit()
            => Client.MakeRequestAsync("/process-control/v1/process/quit", Method.POST);

        public Task Restart()
            => Client.MakeRequestAsync("/process-control/v1/process/restart", Method.POST);
    }
}
