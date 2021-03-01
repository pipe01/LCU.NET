using RestSharp;

namespace LCU.NET
{
    public interface IProxy
    {
        bool Handle<T>(string url, Method method, object data, out T result);
        bool Handle(string url, Method method, object data);
    }
}
