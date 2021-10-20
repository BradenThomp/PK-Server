using Domain.Common.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Domain.Common.Events
{
    /// <summary>
    /// Routes events for an aggregrate based on the convention that each event must have a method of signature <code>void Apply(IEvent @event);</code>.
    /// <example>
    /// Raising the event <see cref="Domain.Features.Tracking.Events.LocationUpdated"/> in <see cref="Domain.Features.Tracking.Tracker"/> has a method of the following signature.
    /// <code>
    ///    private void Apply(LocationUpdated @event)
    ///    {
    ///        Longitude = @event.Longitude;
    ///        Latitude = @event.Latitude;
    ///        LastUpdate = @event.UpdateTime;
    ///    }
    /// </code>
    /// </example>
    /// </summary>
    internal class ConventionalEventRouter : IEventRouter
    {
        private readonly IDictionary<Type, Action<IEvent>> _handlers;

        public ConventionalEventRouter()
        {
            _handlers = new Dictionary<Type, Action<IEvent>>();
        }

        public virtual void Register(IAggregate aggregate)
        {
            var applyMethods = aggregate.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.Name == "Apply" && m.GetParameters().Length == 1 && m.ReturnParameter.ParameterType == typeof(void)).Select(m => new
                {
                    Method = m,
                    MessageType = m.GetParameters().Single().ParameterType,
                });

            foreach(var apply in applyMethods)
            {
                var applyMethod = apply.Method;
                _handlers.Add(apply.MessageType, m => applyMethod.Invoke(aggregate, new[] { m as IEvent }));
            }
        }

        public virtual void Dispatch(IEvent @event)
        {
            Action<IEvent> handler;
            if(_handlers.TryGetValue(@event.GetType(), out handler))
            {
                handler(@event);
            }
            else
            {
                throw new InvalidOperationException($"No handler found for event of type {@event.GetType()}");
            }
        }
    }
}
