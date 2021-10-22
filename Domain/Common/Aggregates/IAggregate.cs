using Domain.Common.Events;
using System.Collections.Generic;

namespace Domain.Common.Aggregates
{
    public interface IAggregate
    {
        int Version { get; }

        void Apply(IEvent @event);

        ICollection<IEvent> GetUncommittedEvents();

        void ClearUncommittedEvents();
    }
}
