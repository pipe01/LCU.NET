using RestSharp;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LCU.NET.Plugins
{
    public static class PluginManager
    {
        private static IDictionary<string, byte[]> Cache = new Dictionary<string, byte[]>();

        public static async Task<byte[]> GetAssetAsync(string plugin, string path, bool cache = true)
        {
            string uri = $"/{plugin}/assets/{path}";

            if (!cache || !Cache.TryGetValue(uri, out var data))
            {
                var req = new RestRequest(uri, Method.GET);
                var resp = await LeagueClient.Client.ExecuteTaskAsync(req);
                Cache[uri] = data = resp.RawBytes;
            }

            return data;
        }
    }
}
