using Domain.Common.Aggregates;
using Domain.Events;
using Domain.Models;
using Domain.Projections;
using System;

namespace Domain.Aggregates
{
    public class Tracker : AggregateBase
    {
        public Location Location { get; private set; }

        public DateTime LastUpdate { get; private set; }

        public Speaker AssignedSpeaker { get; private set; }

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

        public void AssignSpeaker(Speaker speaker)
        {
            Raise(new TrackerAttachedToSpeakerEvent(speaker));
        }

        public void Apply(TrackerAttachedToSpeakerEvent @event)
        {
            AssignedSpeaker = @event.AssignedSpeaker;
            @event.AssignedSpeaker.TrackerId = Id;
        }

        private void Apply(TrackerRegisteredEvent @event)
        {
            MACAddress = @event.MACAddress;
            Location = new Location(0.0, 0.0);
        }

        private void Apply(LocationUpdatedEvent @event)
        {
            Location = @event.Location;
            LastUpdate = @event.UpdateTime;
        }

        public override IProjection CreateProjection()
        {
            return new TrackerProjection(MACAddress, Location.Longitude, Location.Latitude, LastUpdate, AssignedSpeaker);
        }
    }
}
