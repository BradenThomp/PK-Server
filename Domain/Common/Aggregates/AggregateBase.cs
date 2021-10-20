using Domain.Common.Events;
using System.Collections.Generic;

namespace Domain.Common.Aggregates
{
    public abstract class AggregateBase : IAggregate
    {
        private readonly IList<IEvent> _uncommittedEvents;
        private readonly IEventRouter _eventRouter;
        
        protected AggregateBase() : this(new ConventionalEventRouter()) { }

        protected AggregateBase(IEventRouter eventRouter)
        {
            _eventRouter = eventRouter;
            _uncommittedEvents = new List<IEvent>();
            _eventRouter.Register(this);
        }

        public int Version { get; protected set; }

        protected void Raise(IEvent @event)
        {
            Apply(@event);
            _uncommittedEvents.Add(@event);
        }

        private void Apply(IEvent @event)
        {
            _eventRouter.Dispatch(@event);
            Version++;
        }
    }
}
