using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCU.NET
{
    public delegate void ConnectedChangedDelegate(bool connected);

    public interface ILeagueClient
    {
        event ConnectedChangedDelegate ConnectedChanged;

        bool IsConnected { get; }

        IProxy Proxy { get; set; }
        IRestClient Client { get; }
        ILeagueSocket Socket { get; }
        
        void BeginTryInit(InitializeMethod method = InitializeMethod.CommandLine, int interval = 500);
        bool Init(InitializeMethod method = InitializeMethod.CommandLine);
        void Close();

        Task<T> MakeRequestAsync<T>(string resource, Method method, object data = null, Action<IRestRequest> modifyRequest = null, params string[] fields);
        Task MakeRequestAsync(string resource, Method method, object data = null, Action<IRestRequest> modifyRequest = null, params string[] fields);
    }
}
