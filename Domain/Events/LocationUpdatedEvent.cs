using Domain.Common.Events;
using Domain.Models;
using System;

namespace Domain.Events
{
    [Serializable]
    public record LocationUpdatedEvent(Location Location, DateTime UpdateTime) : IEvent;
}
