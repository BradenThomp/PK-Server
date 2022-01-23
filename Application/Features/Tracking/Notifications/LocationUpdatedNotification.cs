using Application.Common.Notifications;
using Domain.Models;
using System;

namespace Application.Features.Tracking.Notifications
{
    public class LocationUpdatedNotification : INotification
    {
        public Location Location { get; }

        public DateTime UpdateTime { get; }

        public string TrackerId { get; }

        public LocationUpdatedNotification(Location location, DateTime updateTime, string trackerId)
        {
            Location = location;
            UpdateTime = updateTime;
            TrackerId = trackerId;
        }
    }
}
