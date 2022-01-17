using Domain.Common.Events;
using Domain.Projections;
using System.Collections.Generic;

namespace Domain.Common.Aggregates
{
    public interface IAggregate
    {
        string Id { get; }

        int Version { get; }

        void Apply(IEvent @event);

        ICollection<IEvent> GetUncommittedEvents();

        void ClearUncommittedEvents();

        IProjection CreateProjection();
    }
}
