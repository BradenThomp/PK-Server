using Application.Common.Repository;
using Domain.Common.Aggregates;
using Domain.Common.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    /// <summary>
    /// Temporary in memory repository until an actual database is setup.
    /// </summary>
    public class InMemoryEventRepository : IEventRepository
    {
        private readonly ICollection<EventRecord> _inMemoryStore;

        public InMemoryEventRepository()
        {
            _inMemoryStore = new List<EventRecord>();
        }

        public async Task<TAggregate> GetByIdAsync<TAggregate>(string id) where TAggregate : IAggregate
        {
            var records = _inMemoryStore.Where(r => r.AggregateId == id).OrderBy(r => r.Version);
            var aggregate = Build<TAggregate>();
            foreach(var record in records)
            {
                var @event = Deserialize(record.Data, record.Type);
                aggregate.Apply(@event);
            }

            return aggregate;
        }

        public Task<TAggregate> GetByIdAsync<TAggregate>(string id, int version) where TAggregate : IAggregate
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync(IAggregate aggregate)
        {
            var events = aggregate.GetUncommittedEvents();
            var version = aggregate.Version - events.Count + 1;
            foreach(var @event in events)
            {
                var json = Serialize(@event);
                var record = new EventRecord(aggregate.Id, version, @event.GetType(), json);
                _inMemoryStore.Add(record);
                version++;
            }
        }

        private TAggregate Build<TAggregate>() where TAggregate : IAggregate
        {
            var type = typeof(TAggregate);
            ConstructorInfo constructor = type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { }, null);
            return (TAggregate)constructor.Invoke(new object[] { });
        }

        private string Serialize(IEvent @event)
        {
            return JsonConvert.SerializeObject(@event);
        }

        private IEvent Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type) as IEvent;
        }
    }

    internal class EventRecord
    {
        public string AggregateId { get; set; }

        public int Version { get; set; }

        public string Data { get; set; }

        public Type Type { get; set; }

        public EventRecord(string aggregateId, int version, Type type, string data)
        {
            AggregateId = aggregateId;
            Version = version;
            Data = data;
            Type = type;
        }
    }
}
