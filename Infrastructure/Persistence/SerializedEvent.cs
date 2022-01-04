using Domain.Common.Events;
using Newtonsoft.Json;
using System;

namespace Infrastructure.Persistence
{
    public class SerializedEvent
    {
        public string AggregateId { get; set; }

        public int Version { get; set; }

        public string Data { get; set; }

        public string Type { get; set; }

        public SerializedEvent() { }

        private SerializedEvent(string aggregateId, int version, string type, string data)
        {
            AggregateId = aggregateId;
            Version = version;
            Data = data;
            Type = type;
        }

        public static SerializedEvent Serialize(IEvent @event, string aggregateId, int version)
        {
            var json = JsonConvert.SerializeObject(@event);
            var serializedType = JsonConvert.SerializeObject(@event.GetType());
            return new SerializedEvent(aggregateId, version, serializedType, json);
        }

        public static IEvent Deserialize(SerializedEvent @event)
        {
            var type = JsonConvert.DeserializeObject(@event.Type, typeof(Type)) as Type;
            return JsonConvert.DeserializeObject(@event.Data, type) as IEvent;
        }
    }
}
