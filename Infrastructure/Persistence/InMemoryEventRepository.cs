using Application.Common.Repository;
using Domain.Common.Aggregates;
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
        private readonly ICollection<SerializedEvent> _inMemoryStore;

        public InMemoryEventRepository()
        {
            _inMemoryStore = new List<SerializedEvent>();
        }

        public async Task<TAggregate> GetByIdAsync<TAggregate>(string id) where TAggregate : IAggregate
        {
            var records = _inMemoryStore.Where(r => r.AggregateId == id).OrderBy(r => r.Version);
            var aggregate = Build<TAggregate>();
            foreach(var record in records)
            {
                var @event = SerializedEvent.Deserialize(record);
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
                var record = SerializedEvent.Serialize(@event, aggregate.Id, version);
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
    }
}
