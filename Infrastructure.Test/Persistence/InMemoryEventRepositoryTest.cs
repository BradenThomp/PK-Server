using Domain.Aggregates;
using Domain.Models;
using Infrastructure.Persistence;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Test.Persistence
{
    public class InMemoryEventRepositoryTest
    {
        [Test]
        public async Task ShouldReturnEquivalentObject_AfterAggregateStateSaved_GetByIdAsync()
        {
            var id = "00:00:5e:00:53:af";
            var repo = new InMemoryEventRepository();
            var aggregate = Tracker.RegisterTracker(id);
            aggregate.UpdateLocation(new Location(0, 0), new DateTime(2021, 7, 12));
            aggregate.UpdateLocation(new Location(20, 20), new DateTime(2021, 7, 13));
            await repo.SaveAsync(aggregate);
            var rebuiltAggregate = await repo.GetByIdAsync<Tracker>(id);
            Assert.That(JsonConvert.SerializeObject(aggregate), Is.EqualTo(JsonConvert.SerializeObject(rebuiltAggregate)));
        }
    }
}
