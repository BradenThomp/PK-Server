//using Domain.Aggregates;
using Domain.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;

namespace Domain.Test.Features.Tracking
{
    /**public class TrackerTest
    {
        private Tracker _sytemUnderTest;
        
        [SetUp]
        public void Setup()
        {
            _sytemUnderTest = Tracker.RegisterTracker("00:00:5e:00:53:af");
        }

        [Test]
        public void Longitude_UpdateLocation_ReturnsUpdatedLongitude()
        {
            _sytemUnderTest.UpdateLocation(new Location(30.12, -32.531234), new DateTime(2021, 7, 12));
            Assert.That(_sytemUnderTest.Location.Longitude, Is.EqualTo(30.12));
        }

        [Test]
        public void Latitude_UpdateLocation_ReturnsUpdatedLatitude()
        {
            _sytemUnderTest.UpdateLocation(new Location(30.12, -32.531234), new DateTime(2021, 7, 12));
            Assert.That(_sytemUnderTest.Location.Latitude, Is.EqualTo(-32.531234));
        }

        [Test]
        public void LastUpdate_UpdateLocation_ReturnsUpdatedDateTime()
        {
            _sytemUnderTest.UpdateLocation(new Location(30.12, -32.531234), new DateTime(2021, 7, 12));
            Assert.That(_sytemUnderTest.LastUpdate, Is.EqualTo(new DateTime(2021, 7, 12)));
        }

        [Test]
        public void GetUncommittedEvents_AfterTwoUpdates_ReturnsTwoUncommittedEvents()
        {
            _sytemUnderTest.UpdateLocation(new Location(30.12, -32.531234), new DateTime(2021, 7, 12));
            _sytemUnderTest.UpdateLocation(new Location(30.12, -32.531234), new DateTime(2021, 7, 13));
            Assert.That(_sytemUnderTest.GetUncommittedEvents().Count, Is.EqualTo(3));
        }
    }**/
}
