using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCU.NET.WAMP
{
    public struct JsonApiEvent
    {
        [JsonProperty("eventType")]
        public EventType EventType { get; }

        [JsonProperty("uri")]
        public string URI { get; }

        [JsonProperty("data")]
        public JToken Data { get; }

        [JsonConstructor]
        internal JsonApiEvent(EventType eventType, string uri, JToken data)
        {
            this.EventType = eventType;
            this.URI = uri;
            this.Data = data;
        }

        public JsonApiEvent(EventType eventType, string uri, object data)
        {
            this.EventType = eventType;
            this.URI = uri;
            this.Data = new JObject(data);
        }

        internal T GetData<T>() => Data.ToObject<T>();

        internal object GetData(Type type) => Data.ToObject(type, new JsonSerializer { NullValueHandling = NullValueHandling.Ignore });

        internal static JsonApiEvent Parse(string json)
        {
            var p = JArray.Parse(json);

            if (p.Count != 3)
                return default;

            int type = p[0].ToObject<int>();
            string realm = p[1].ToObject<string>();

            if (type == 8 && realm == "OnJsonApiEvent")
            {
                return p[2].ToObject<JsonApiEvent>();
            }

            return default;
        }
    }
}
