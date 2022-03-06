using Domain.Common.Exceptions;
using Domain.Common.Models;
using System;

namespace Domain.Models
{
    public class Speaker : IModel
    {
        public string SerialNumber { get; init; }

        public string Model { get; init; }

        public Tracker Tracker { get; set; }

        public Guid? RentalId { get; set; }

        public Speaker() { }

        public Speaker(string serialNumber, string model)
        {
            SerialNumber = serialNumber;
            Model = model;
            RentalId = null;
        }

        public void AttachTracker(Tracker tracker)
        {
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
            return result;
        }
    }
}
