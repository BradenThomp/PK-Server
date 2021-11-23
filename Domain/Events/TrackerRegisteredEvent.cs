using Domain.Common.Events;
using System;

namespace Domain.Events
{
    [Serializable]
    public record TrackerRegisteredEvent(string MACAddress) : IEvent;
}
