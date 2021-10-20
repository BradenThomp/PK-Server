using Domain.Common.Aggregates;

namespace Domain.Common.Events
{
    public interface IEventRouter
    {
        void Dispatch(IEvent @event);

        void Register(IAggregate aggregate);
    }
}
