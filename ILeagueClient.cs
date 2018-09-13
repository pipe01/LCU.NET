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

        bool SmartInit();
        void BeginTryInit();
        bool Init();
        void Close();

        Task<T> MakeRequestAsync<T>(string resource, Method method, object data = null, params string[] fields);
        Task MakeRequestAsync(string resource, Method method, object data = null, params string[] fields);
    }
}
