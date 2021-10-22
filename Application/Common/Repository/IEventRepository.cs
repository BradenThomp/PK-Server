using Domain.Common.Aggregates;
using System;
using System.Threading.Tasks;

namespace Application.Common.Repository
{
    public interface IEventRepository
    {
        Task<TAggregate> GetByIdAsync<TAggregate>(Guid id) where TAggregate : IAggregate;

        Task<TAggregate> GetByIdAsync<TAggregate>(Guid id, int version) where TAggregate : IAggregate;

        Task SaveAsync(IAggregate aggregate);
    }
}
