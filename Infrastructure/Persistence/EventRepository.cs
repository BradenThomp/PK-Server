using Application.Common.Repository;
using Dapper;
using Domain.Common.Aggregates;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class EventRepository : IEventRepository
    {
        private readonly IConfiguration _configuration;

        public EventRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<TAggregate> GetByIdAsync<TAggregate>(string id) where TAggregate : IAggregate
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                var serializedEvents = await connection.QueryAsync<SerializedEvent>("SELECT * FROM serialized_event WHERE aggregate_id = @Id ORDER BY version", new { Id = id });
                var aggregate = Build<TAggregate>();
                foreach (var serializedEvent in serializedEvents)
                {
                    var @event = SerializedEvent.Deserialize(serializedEvent);
                    aggregate.Apply(@event);
                }

                return aggregate;
            }
        }

        public Task<TAggregate> GetByIdAsync<TAggregate>(string id, int version) where TAggregate : IAggregate
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync(IAggregate aggregate)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                var events = aggregate.GetUncommittedEvents();
                var version = aggregate.Version - events.Count + 1;
                foreach (var @event in events)
                {
                    var serializedEvent = SerializedEvent.Serialize(@event, aggregate.Id, version);
                    await connection.ExecuteAsync("INSERT INTO serialized_event(aggregate_id, version, data, type) VALUES(@AggregateId, @Version, @Data, @Type)", new { AggregateId=serializedEvent.AggregateId, Version=serializedEvent.Version, Data=serializedEvent.Data, Type= serializedEvent.Type});
                    version++;
                }
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
