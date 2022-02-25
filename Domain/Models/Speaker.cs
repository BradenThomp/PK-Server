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
            Tracker = tracker;
            Tracker.SpeakerSerialNumber = SerialNumber;
        }

        public ReturnedSpeaker Return()
        {
            if(RentalId is null)
            {
                throw new Exception("Speaker could not be returned as it is not currently rented.");
            }
            var result = new ReturnedSpeaker(SerialNumber, Model, RentalId.Value, DateTime.UtcNow);
            Tracker.SpeakerSerialNumber = null;
            Tracker = null;
            RentalId = null;
            return result;
        }
    }
}
