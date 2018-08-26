using LCU.NET.API_Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

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

        private static bool _Connected;
        public static bool Connected
        {
            get => _Connected;
            private set
            {
                _Connected = value;
                ConnectedChanged?.Invoke(value);
            }
        }

        public static IProxy Proxy { get; set; }

        public delegate void ConnectedChangedDelegate(bool connected);
        public static event ConnectedChangedDelegate ConnectedChanged;

        /// <summary>
        /// Tries to get the LoL installation path and init. The client must be running.
        /// </summary>
        public static bool TryInit()
        {
            return TryInitInner(true);
        }

        private static bool TryInitInner(bool @catch = true)
        {
            var p = Process.GetProcessesByName("LeagueClient");

            if (p.Length > 0)
            {
                //May lord forgive me
                try
                {
                    return Init(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(p[0].MainModule.FileName), "../../../../../../")));
                }
                catch (Exception)
                {
                    if (@catch)
                    {
                        Thread.Sleep(1000);
                        return TryInitInner(false);
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Begins to look for the LoL client and inits when detected.
        /// </summary>
        public static void BeginTryInit()
        {
            new Thread(() =>
            {
                while (!TryInit())
                {
                    Thread.Sleep(500);
                }
            })
            {
                IsBackground = true
            }.Start();
        }

        public static bool Init(string lolPath)
        {
            if (Connected)
                return false;

            string lockFilePath = Path.Combine(lolPath, "lockfile");

            if (!File.Exists(lockFilePath))
                return false;

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

            Connected = true;

            return true;
        }

        internal static void Close()
        {
            Connected = false;
        }

        private static void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            if (e.Name == "lockfile")
            {
                Connected = false;
            }
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

        public static async Task<T> MakeRequestAsync<T>(string resource, Method method, object data = null) where T : new()
        {
            if (Proxy != null && Proxy.Handle<T>(resource, method, data, out var ret))
                return ret;

            if (!Connected)
                return default;

            var resp = await Client.ExecuteTaskAsync(BuildRequest(resource, method, data));
            CheckErrors(resp);

            return JsonConvert.DeserializeObject<T>(resp.Content, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public static async Task MakeRequestAsync(string resource, Method method, object data = null)
        {
            if (Proxy?.Handle(resource, method, data) == true)
                return;

            if (!Connected)
                return;

            var resp = await Client.ExecuteTaskAsync(BuildRequest(resource, method, data));
            CheckErrors(resp);
        }

        internal static Task<T> MakeRequestAsync<T>(object data = null, [CallerMemberName] string methodName = "") where T : new()
        {
            var apiAttr = GetCallingAPI(methodName);

            Task<T> act() => MakeRequestAsync<T>(apiAttr.URI, apiAttr.Method, data);

            return apiAttr.Cache ? Cache(act) : act();
        }

        internal static Task MakeRequestAsync(object data = null, [CallerMemberName] string methodName = "")
        {
            var apiAttr = GetCallingAPI(methodName);

            return MakeRequestAsync(apiAttr.URI, apiAttr.Method, data);
        }

        private static APIMethodAttribute GetCallingAPI(string methodName, bool @throw = true)
        {
            var trace = new StackTrace();

            for (int i = 0; i < trace.FrameCount; i++)
            {
                var method = trace.GetFrame(i).GetMethod();

                if (method.Name == methodName)
                {
                    var attr = method.GetCustomAttribute<APIMethodAttribute>();

                    if (attr != null)
                    {
                        return attr;
                    }
                }
            }

            if (@throw)
                throw new InvalidOperationException("This method can only be called from a method with an APIMethodAttribute!");

            return null;
        }

        private static void CheckErrors(IRestResponse response)
        {
            if (response.Content.Contains("\"errorCode\""))
            {
                var error = JsonConvert.DeserializeObject<ErrorData>(response.Content);

                if (error.Message == "No active delegate")
                    throw new NoActiveDelegateException(error);

                throw new APIErrorException(error);
            }
        }

        internal static T Cache<T>(Func<T> action)
        {
            var method = new StackFrame(1).GetMethod();

            if (method.DeclaringType == typeof(LeagueClient))
                method = new StackFrame(2).GetMethod();

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
