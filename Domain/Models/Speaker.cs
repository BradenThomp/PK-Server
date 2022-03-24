using Domain.Common.Exceptions;
using Domain.Common.Models;
using System;

namespace Domain.Models
{
    /// <summary>
    /// Represents a speaker that can be rented by customers.
    /// </summary>
    public class Speaker : IModel
    {
        /// <summary>
        /// The unique serial number of the speaker.
        /// </summary>
        public string SerialNumber { get; init; }

        /// <summary>
        /// The model of the speaker.
        /// </summary>
        public string Model { get; init; }

        /// <summary>
        /// The tracker being used to track the speaker when it is rented.
        /// </summary>
        public Tracker Tracker { get; set; }

        /// <summary>
        /// The id of the rental that the speaker currently belongs to.
        /// </summary>
        public Guid? RentalId { get; set; }

        /// <summary>
        /// Flag that marks whether or not the speaker has reached its rental destination.
        /// </summary>
        public bool ReachedDestination { get; set; }

        public Speaker() { }

        public Speaker(string serialNumber, string model)
        {
            SerialNumber = serialNumber;
            Model = model;
            RentalId = null;
            ReachedDestination = false;
        }

        /// <summary>
        /// Attaches a tracker to the speaker if the speaker is not currently rented.
        /// </summary>
        /// <param name="tracker">The tracker to attach.</param>
        public void AttachTracker(Tracker tracker)
        {
            if(tracker is null)
            {
                throw new DomainValidationException($"Empty trackers cannot be attached to speakers.");
            }
            if (!string.IsNullOrEmpty(tracker.SpeakerSerialNumber))
            {
                throw new DomainValidationException($"Could not assign tracker {tracker.HardwareId} to speaker {SerialNumber} as the tracker is currently in use.");
            }
            if(Tracker is not null)
            {
                throw new DomainValidationException($"Could not assign tracker {tracker.HardwareId} to speaker {SerialNumber} as the speaker is already being tracked.");
            }
            Tracker = tracker;
            Tracker.SpeakerSerialNumber = SerialNumber;
        }

        /// <summary>
        /// Returns the speaker if it is currently rented.
        /// </summary>
        /// <returns>A returned speaker record.</returns>
        public ReturnedSpeaker Return()
        {
            if(RentalId is null)
            {
                throw new DomainValidationException("Speaker could not be returned as it is not currently rented.");
            }
            var result = new ReturnedSpeaker(SerialNumber, Model, RentalId.Value, DateTime.UtcNow);
            Tracker.SpeakerSerialNumber = null;
            Tracker = null;
            RentalId = null;
            ReachedDestination = false;
            return result;
        }
    }
}
