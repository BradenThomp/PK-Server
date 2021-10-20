using Domain.Common.Events;
using System;

namespace Domain.Features.Tracking.Events
{
    public record LocationUpdated(Guid TrackerId, double Longitude, double Latitude, DateTime UpdateTime) : IEvent;
}
