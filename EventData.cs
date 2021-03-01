using LCU.NET.WAMP;
using Newtonsoft.Json;
using System;

namespace LCU.NET
{
    public struct EventData
    {
        public DateTime Time { get; }
        public DateTime RecordingStartTime { get; }
        public JsonApiEvent JsonEvent { get; }

        [JsonIgnore]
        public TimeSpan TimeSinceStart => Time.Subtract(RecordingStartTime);

        [JsonConstructor]
        public EventData(DateTime time, DateTime recordingStartTime, JsonApiEvent jsonEvent)
        {
            this.Time = time;
            this.RecordingStartTime = recordingStartTime;
            this.JsonEvent = jsonEvent;
        }

        public EventData(DateTime recordingStartTime, JsonApiEvent jsonEvent) 
            : this(DateTime.Now, recordingStartTime, jsonEvent)
        {
        }
    }
}
