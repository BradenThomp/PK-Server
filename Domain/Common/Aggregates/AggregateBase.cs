using Domain.Common.Events;
using System.Collections.Generic;

namespace Domain.Common.Aggregates
{
    public abstract class AggregateBase : IAggregate
    {
        private readonly ICollection<IEvent> _uncommittedEvents;
        private readonly IEventRouter _eventRouter;

        public int Version { get; protected set; }

        public virtual string Id { get; }

        protected AggregateBase() : this(new ConventionalEventRouter()) { }

        protected AggregateBase(IEventRouter eventRouter)
        {
            _eventRouter = eventRouter;
            _uncommittedEvents = new List<IEvent>();
            _eventRouter.Register(this);
        }

        protected void Raise(IEvent @event)
        {
            Apply(@event);
            _uncommittedEvents.Add(@event);
        }

        public void Apply(IEvent @event)
        {
            _eventRouter.Dispatch(@event);
            Version++;
        }

        public ICollection<IEvent> GetUncommittedEvents()
        {
            return _uncommittedEvents;
        }

        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }
    }
}
