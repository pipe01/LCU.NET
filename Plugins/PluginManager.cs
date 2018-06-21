using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCU.NET.Plugins
{
    public static class PluginManager
    {
        public static byte[] GetAsset(string plugin, string path)
        {
            var req = new RestRequest($"/{plugin}/assets/{path}", Method.GET);
            var resp = LeagueClient.Client.Execute(req);

            return resp.RawBytes;
        }
    }
}
