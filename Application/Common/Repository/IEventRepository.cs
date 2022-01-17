using Domain.Common.Aggregates;
using System;
using System.Threading.Tasks;

namespace Application.Common.Repository
{
    public interface IEventRepository
    {
        Task<TAggregate> GetByIdAsync<TAggregate>(string id) where TAggregate : IAggregate;

        Task<TAggregate> GetByIdAsync<TAggregate>(string id, int version) where TAggregate : IAggregate;

        Task SaveAsync<TAggregate>(TAggregate aggregate) where TAggregate : IAggregate;
    }
}
