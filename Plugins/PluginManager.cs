using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LCU.NET.Plugins
{
    public interface IPluginManager
    {
        Task<byte[]> GetAssetAsync(string plugin, string path, bool cache = true);
    }

    public class PluginManager : IPluginManager
    {
        private readonly ILeagueClient Client;
        public PluginManager(ILeagueClient client)
        {
            this.Client = client;
        }

        private readonly IDictionary<string, byte[]> Cache = new Dictionary<string, byte[]>();

        public async Task<byte[]> GetAssetAsync(string plugin, string path, bool cache = true)
        {
            string uri = $"/{plugin}/assets/{path}";

            if (!cache || !Cache.TryGetValue(uri, out var data))
            {
                var req = new RestRequest(uri, Method.GET);
                var resp = await Client.Client.ExecuteTaskAsync(req);
                Cache[uri] = data = resp.RawBytes;
            }

            return data;
        }
    }
}
