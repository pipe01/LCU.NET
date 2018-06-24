using RestSharp;
using System;

namespace LCU.NET
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    internal sealed class APIMethodAttribute : Attribute
    {
        public APIMethodAttribute(string uri, Method method)
        {
            this.URI = uri;
            this.Method = method;
        }

        public string URI { get; }
        public Method Method { get; }

        public bool Cache { get; set; }
    }
}
