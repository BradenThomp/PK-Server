using Domain.Common.Exceptions;
using Domain.Models;
using NUnit.Framework;
using System;

namespace Domain.Test.Models
{
    public class SpeakerTest
    {
        [Test]
        public void ShouldThrowDomainException_ReturnSpeakerThatIsNotRented()
        {
            Speaker s = new Speaker("sn-12387e2","Clarity");
            Assert.Throws<DomainValidationException>(() => s.Return());
        }

        [Test]
        public void ShouldThrowDomainException_AttachNullTracker()
        {
            Speaker s = new Speaker("sn-12387e2", "Clarity");
            Assert.Throws<DomainValidationException>(() => s.AttachTracker(null));
        }

        [Test]
        public void ShouldThrowDomainException_AttachATrackerIfSpeakerIsAlreadyRented()
        {
            Speaker s = new Speaker("sn-12387e2", "Clarity");
            Tracker t1 = new Tracker()
            {
                HardwareId = "1",
                Location = new Location(0, 0),
            };
            Tracker t2 = new Tracker()
            {
                HardwareId = "1",
                Location = new Location(0, 0),
            };
            s.AttachTracker(t1);
            Assert.Throws<DomainValidationException>(() => s.AttachTracker(t2));
        }

        [Test]
        public void ShouldThrowDomainException_AttachATrackerToMultipleSpeakers()
        {
            Speaker s1 = new Speaker("sn-12387e2", "Clarity");
            Speaker s2 = new Speaker("sn-12387e3", "Clarity");
            Tracker t = new Tracker()
            {
                HardwareId = "1",
                Location = new Location(0, 0),
            };
            s2.AttachTracker(t);
            Assert.Throws<DomainValidationException>(() => s1.AttachTracker(t));
        }

        [Test]
        public void ShouldAssignTracker_AttachTrackerToSpeaker()
        {
            Speaker s = new Speaker("sn-12387e2", "Clarity");
            Tracker t = new Tracker()
            {
                HardwareId = "1",
                Location = new Location(0, 0),
            };
            s.AttachTracker(t);
            Assert.That(s.Tracker, Is.EqualTo(t));
        }

        [Test]
        public void ShouldReturnSpeaker_ReturnASpeakerThatIsBeingTracked()
        {
            Speaker s = new Speaker("sn-12387e2", "Clarity");
            var rentalId = Guid.NewGuid();
            s.RentalId = rentalId;
            Tracker t = new Tracker()
            {
                HardwareId = "1",
                Location = new Location(0, 0),
            };
            s.AttachTracker(t);
            var returnedSpeaker = s.Return();
            Assert.That(s.Tracker, Is.Null);
            Assert.That(s.RentalId, Is.Null);
            Assert.That(returnedSpeaker.RentalId, Is.EqualTo(rentalId));
        }
    }
}
