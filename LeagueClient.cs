using LCU.NET.API_Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace LCU.NET
{
    public static class LeagueClient
    {
        private static string Token;
        private static int Port;
        private static int PID;
        private static IDictionary<string, object> CacheDic = new Dictionary<string, object>();
        private static FileSystemWatcher Watcher;

        internal static RestClient Client;

        /// <summary>
        /// Tries to get the LoL installation path and init. The client must be running.
        /// </summary>
        public static bool TryInit()
        {
            var p = Process.GetProcessesByName("LeagueClient");

            if (p.Length > 0)
            {
                //May lord forgive me
                Init(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(p[0].MainModule.FileName), "../../../../../../")));

                return true;
            }

            return false;
        }

        public static void Init(string lolPath)
        {
            string lockFilePath = Path.Combine(lolPath, "lockfile");

            if (!File.Exists(lockFilePath))
                return;

            string lockFile;

            using (var stream = File.Open(lockFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                lockFile = new StreamReader(stream).ReadToEnd();

            Watcher = new FileSystemWatcher(lolPath, "lockfile");
            Watcher.Deleted += Watcher_Deleted;

            string[] parts = lockFile.Split(':');

            PID = int.Parse(parts[1]);
            Port = int.Parse(parts[2]);
            Token = parts[3];

            Client = new RestClient("https://127.0.0.1:" + Port);
            Client.Authenticator = new HttpBasicAuthenticator("riot", Token);
            Client.ConfigureWebRequest(o =>
            {
                o.Accept = "application/json";
#pragma warning disable RCS1163 // Unused parameter.
                o.ServerCertificateValidationCallback = (a, b, c, d) => true;
#pragma warning restore RCS1163 // Unused parameter.
            });

            LeagueSocket.Init(Port, Token);
        }

        private static void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static RestRequest BuildRequest(string resource, Method method, object data)
        {
            var req = new RestRequest(resource, method);

            if (data != null)
            {
                req.AddHeader("Content-Type", "application/json");
                req.AddJsonBody(data);
            }

            return req;
        }

        internal static T MakeRequest<T>(string resource, Method method, object data = null) where T : new()
        {
            var resp = Client.Execute(BuildRequest(resource, method, data));
            CheckErrors(resp);

            return JsonConvert.DeserializeObject<T>(resp.Content, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        internal static void MakeRequest(string resource, Method method, object data = null)
        {
            var resp = Client.Execute(BuildRequest(resource, method, data));
            CheckErrors(resp);
        }

        private static void CheckErrors(IRestResponse response)
        {
            if (response.Content.Contains("\"errorCode\""))
            {
                var error = JsonConvert.DeserializeObject<ErrorData>(response.Content);
                throw new ApiErrorException(error);
            }
        }

        internal static T Cache<T>(Func<T> action)
        {
            var method = new StackFrame(1).GetMethod();

            return Cache(method.DeclaringType.Name + "." + method.Name, action);
        }

        internal static T Cache<T>(string id, Func<T> action)
        {
            if (!CacheDic.TryGetValue(id, out var val))
            {
                val = CacheDic[id] = action();
            }

            return (T)val;
        }
    }
}
