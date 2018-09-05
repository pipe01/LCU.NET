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
    public sealed class LeagueClient : ILeagueClient
    {
        public static ILeagueClient Default { get; set; } = new LeagueClient();

        private static IDictionary<string, object> CacheDic = new Dictionary<string, object>();

        private string Token;
        private int Port;

        private bool _Connected;
        public bool IsConnected
        {
            get => _Connected;
            private set
            {
                _Connected = value;
                ConnectedChanged?.Invoke(value);
            }
        }

        public IProxy Proxy { get; set; }
        public IRestClient Client { get; private set; }

        public event ConnectedChangedDelegate ConnectedChanged;

        /// <summary>
        /// Tries to get the LoL installation path and init. The client must be running.
        /// </summary>
        public bool SmartInit()
        {
            return TryInitInner();
        }

        private bool TryInitInner()
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
        public void BeginTryInit()
        {
            new Thread(() =>
            {
                while (!SmartInit())
                {
                    Thread.Sleep(500);
                }
            })
            {
                IsBackground = true
            }.Start();
        }

        public bool Init()
        {
            if (IsConnected)
                return false;

            var process = Process.GetProcessesByName("LeagueClientUx").FirstOrDefault();

            if (process == null)
                return false;

            string cmdLine = GetCommandLine(process);

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

            IsConnected = true;

            return true;
        }

        public void Close()
        {
            IsConnected = false;
            LeagueSocket.Close();
        }

        private static string GetCommandLine(Process process)
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

        public async Task<T> MakeRequestAsync<T>(string resource, Method method, object data = null, params string[] fields)
        {
            if (Proxy != null && Proxy.Handle<T>(resource, method, data, out var ret))
                return ret;

            if (!IsConnected)
                return default;

            var resp = await Client.ExecuteTaskAsync(BuildRequest(resource, method, data, fields));
            CheckErrors(resp);

            return JsonConvert.DeserializeObject<T>(resp.Content, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public async Task MakeRequestAsync(string resource, Method method, object data = null, params string[] fields)
        {
            if (Proxy?.Handle(resource, method, data) == true)
                return;

            if (!IsConnected)
                return;

            var resp = await Client.ExecuteTaskAsync(BuildRequest(resource, method, data, fields));
            CheckErrors(resp);
        }

        internal static Task<T> MakeRequestAsync<T>(object data = null, [CallerMemberName] string methodName = "", params string[] args)
        {
            var apiAttr = GetCallingAPI(methodName);

            Task<T> act() => Default.MakeRequestAsync<T>(ReplaceArgs(apiAttr.URI, args), apiAttr.Method, data);

            return apiAttr.Cache ? Cache(act) : act();
        }

        internal static Task MakeRequestAsync(object data = null, [CallerMemberName] string methodName = "", params string[] args)
        {
            var apiAttr = GetCallingAPI(methodName);

            return Default.MakeRequestAsync(ReplaceArgs(apiAttr.URI, args), apiAttr.Method, data);
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
