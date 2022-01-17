using Application.Common.Repository;
using Dapper;
using Domain.Common.Aggregates;
using Infrastructure.Persistence.Projections;
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
        private readonly IServiceProvider _serviceProvider;

        public EventRepository(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
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

        public async Task SaveAsync<TAggregate>(TAggregate aggregate) where TAggregate : IAggregate
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
                // Saves a snapshot of the aggregate so it is easier to access when querying.
                var projectionWriter = _serviceProvider.GetService(typeof(IProjectionWriter<TAggregate>)) as IProjectionWriter<TAggregate>;
                await projectionWriter.SaveProjection(aggregate);
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
