using LCU.NET.WAMP;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCU.NET
{
    public interface IProxy
    {
        bool Handle<T>(string url, Method method, object data, out T result);
        bool Handle(string url, Method method, object data);

        JsonApiEvent Handle(JsonApiEvent @event);
    }
}
