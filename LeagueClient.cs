using LCU.NET.API_Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace LCU.NET
{
    public static class LeagueClient
    {
        private static string Token;
        private static int Port;
        private static IDictionary<string, object> CacheDic = new Dictionary<string, object>();

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
                return Init();
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

        public static bool Init()
        {
            if (Connected)
                return false;

            var process = Process.GetProcessesByName("LeagueClientUx").FirstOrDefault();

            if (process == null)
                return false;

            string cmdLine = process.GetCommandLine();

            if (cmdLine == null)
                return false;

            string portStr = Regex.Match(cmdLine, @"(?<=--app-port=)\d+").Value;
            Port = int.Parse(portStr);
            Token = Regex.Match(cmdLine, "(?<=--remoting-auth-token=).*?(?=\")").Value;

            process.Exited += (a, b) => Close();

            Client = new RestClient("https://127.0.0.1:" + Port);
            Client.Authenticator = new HttpBasicAuthenticator("riot", Token);
            Client.ConfigureWebRequest(o =>
            {
                o.Accept = "application/json";
                o.ServerCertificateValidationCallback = delegate { return true; };
            });

            LeagueSocket.Init(Port, Token);

            Connected = true;

            return true;
        }

        internal static void Close()
        {
            Connected = false;
            LeagueSocket.Close();
        }

        private static string GetCommandLine(this Process process)
        {
            using (var searcher = new ManagementObjectSearcher(
                $"SELECT CommandLine FROM Win32_Process WHERE ProcessId = {process.Id}"))
            using (var objects = searcher.Get())
            {
                return objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
            }

        }

        private static RestRequest BuildRequest(string resource, Method method, object data, string[] fields = null)
        {
            var req = new RestRequest(resource, method);

            if (data != null)
            {
                object realData = data;

                if (fields?.Length != 0)
                {
                    var dic = new Dictionary<string, object>();

                    foreach (var item in data.GetType().GetProperties().Where(o => fields.Contains(o.Name)))
                    {
                        dic[item.Name] = item.GetValue(data);
                    }

                    realData = dic;
                }

                req.AddHeader("Content-Type", "application/json");
                req.AddJsonBody(realData);
            }

            return req;
        }

        public static async Task<T> MakeRequestAsync<T>(string resource, Method method, object data = null, params string[] fields)
        {
            if (Proxy != null && Proxy.Handle<T>(resource, method, data, out var ret))
                return ret;

            if (!Connected)
                return default;

            var resp = await Client.ExecuteTaskAsync(BuildRequest(resource, method, data, fields));
            CheckErrors(resp);

            return JsonConvert.DeserializeObject<T>(resp.Content, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public static async Task MakeRequestAsync(string resource, Method method, object data = null, params string[] fields)
        {
            if (Proxy?.Handle(resource, method, data) == true)
                return;

            if (!Connected)
                return;

            var resp = await Client.ExecuteTaskAsync(BuildRequest(resource, method, data, fields));
            CheckErrors(resp);
        }

        internal static Task<T> MakeRequestAsync<T>(object data = null, [CallerMemberName] string methodName = "", params string[] args)
        {
            var apiAttr = GetCallingAPI(methodName);

            Task<T> act() => MakeRequestAsync<T>(ReplaceArgs(apiAttr.URI, args), apiAttr.Method, data);

            return apiAttr.Cache ? Cache(act) : act();
        }

        internal static Task MakeRequestAsync(object data = null, [CallerMemberName] string methodName = "", params string[] args)
        {
            var apiAttr = GetCallingAPI(methodName);

            return MakeRequestAsync(ReplaceArgs(apiAttr.URI, args), apiAttr.Method, data);
        }

        private static string ReplaceArgs(string url, string[] args)
        {
            int i = 0;
            return Regex.Replace(url, "{.*?}", o => o.Result(args[i++]));
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
