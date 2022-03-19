using Domain.Common.Exceptions;
using Domain.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Test.Models
{
    public class RentalTest
    {
        [Test]
        public void ShouldReturnSpeakers_SpeakersArePartOfRental()
        {
            var sn1 = new Speaker("sn-1", "Clarity");
            var sn2 = new Speaker("sn-2", "Clarity");
            sn1.AttachTracker(new Tracker()
            {
                HardwareId = "1",
                Location = new Location(0, 0)
            });
            sn2.AttachTracker(new Tracker()
            {
                HardwareId = "2",
                Location = new Location(0, 0)
            });
            var speakers = new List<Speaker>()
            {
                sn1,
                sn2
            };
            Rental r = new Rental(speakers, new Customer(), new DateTime(2022, 4, 17), new DateTime(2022, 4, 18), new Venue());
            r.ReturnSpeakers(new string[] { "sn-1" });
            Assert.That(r.ReturnedSpeakers.Count(), Is.EqualTo(1));
            Assert.That(r.RentedSpeakers.Count(), Is.EqualTo(1));
            Assert.That(r.DateReturned, Is.Null);
            r.ReturnSpeakers(new string[] { "sn-2" });
            Assert.That(r.ReturnedSpeakers.Count(), Is.EqualTo(2));
            Assert.That(r.RentedSpeakers.Count(), Is.EqualTo(0));
            Assert.That(r.DateReturned, Is.Not.Null);
        }

        [Test]
        public void ShouldThrowDomainValidationException_SpeakerWasNeverPartOfRental()
        {
            var sn1 = new Speaker("sn-1", "Clarity");
            var sn2 = new Speaker("sn-2", "Clarity");
            sn1.AttachTracker(new Tracker()
            {
                HardwareId = "1",
                Location = new Location(0, 0)
            });
            sn2.AttachTracker(new Tracker()
            {
                HardwareId = "2",
                Location = new Location(0, 0)
            });
            var speakers = new List<Speaker>()
            {
                sn1,
                sn2
            };
            Rental r = new Rental(speakers, new Customer(), new DateTime(2022, 4, 17), new DateTime(2022, 4, 18), new Venue());
            Assert.Throws<DomainValidationException>(() => r.ReturnSpeakers(new string[] { "sn-3" }));
        }

        [Test]
        public void ShouldThrowDomainValidationException_SpeakerWasReturnedTwice()
        {
            var sn1 = new Speaker("sn-1", "Clarity");
            var sn2 = new Speaker("sn-2", "Clarity");
            sn1.AttachTracker(new Tracker()
            {
                HardwareId = "1",
                Location = new Location(0, 0)
            });
            sn2.AttachTracker(new Tracker()
            {
                HardwareId = "2",
                Location = new Location(0, 0)
            });
            var speakers = new List<Speaker>()
            {
                sn1,
                sn2
            };
            Rental r = new Rental(speakers, new Customer(), new DateTime(2022, 4, 17), new DateTime(2022, 4, 18), new Venue());
            r.ReturnSpeakers(new string[] { "sn-2" });
            Assert.Throws<DomainValidationException>(() => r.ReturnSpeakers(new string[] { "sn-2" }));
        }
    }
}
