using RestSharp;
using System.Collections;
using System.Collections.Generic;

namespace LCU.NET.Plugins
{
    public static class PluginManager
    {
        private static IDictionary<string, byte[]> Cache = new Dictionary<string, byte[]>();

        public static byte[] GetAsset(string plugin, string path, bool cache = true)
        {
            string uri = $"/{plugin}/assets/{path}";

            if (!cache || !Cache.TryGetValue(uri, out var data))
            {
                var req = new RestRequest(uri, Method.GET);
                var resp = LeagueClient.Client.Execute(req);
                Cache[uri] = data = resp.RawBytes;
            }

            return data;
        }
    }
}
