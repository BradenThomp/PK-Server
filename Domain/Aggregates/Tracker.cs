﻿using Domain.Common.Aggregates;
using Domain.Events;
using Domain.Models;
using System;

namespace Domain.Aggregates
{
    public class Tracker : AggregateBase
    {
        public Location Location { get; private set; }

        public DateTime LastUpdate { get; private set; }

        public string MACAddress { get; private set; }

        public override string Id { get => MACAddress; }
        
        /// <summary>
        /// Constructor used for rebuilding aggregate from event history. This should not be used to register new Tracker objects for the system.
        /// </summary>
        private Tracker() { }
        private Tracker(string macAddress)
        {
            Raise(new TrackerRegisteredEvent(macAddress));
        }

        public static Tracker RegisterTracker(string macAddress)
        {
            return new Tracker(macAddress);
        }

        public void UpdateLocation(Location location, DateTime updateTime)
        {
            Raise(new LocationUpdatedEvent(location, updateTime));
        }

        private void Apply(TrackerRegisteredEvent @event)
        {
            MACAddress = @event.MACAddress;
        }

        private void Apply(LocationUpdatedEvent @event)
        {
            Location = @event.Location;
            LastUpdate = @event.UpdateTime;
        }
    }
}
