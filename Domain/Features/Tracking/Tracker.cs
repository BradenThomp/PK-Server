using Domain.Common.Aggregates;
using Domain.Features.Tracking.Events;
using System;

namespace Domain.Features.Tracking
{
    public class Tracker : AggregateBase
    {
        public double Longitude { get; private set; }

        public double Latitude { get; private set; }

        public DateTime LastUpdate { get; private set; }

        public Guid Id { get; init; }

        public void UpdateLocation(double longitude, double latitude, DateTime updateTime)
        {
            Raise(new LocationUpdated(Id, longitude, latitude, updateTime));
        }

        private void Apply(LocationUpdated @event)
        {
            Longitude = @event.Longitude;
            Latitude = @event.Latitude;
            LastUpdate = @event.UpdateTime;
        }
    }
}
