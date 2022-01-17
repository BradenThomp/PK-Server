using Domain.Common.Aggregates;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Projections
{
    public interface IProjectionWriter<TAggregate> where TAggregate : IAggregate
    {
        Task<int> SaveProjection(TAggregate aggregate);
    }
}
