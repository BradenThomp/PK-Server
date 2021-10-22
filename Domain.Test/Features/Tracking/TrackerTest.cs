using Domain.Features.Tracking;
using NUnit.Framework;
using System;

namespace Domain.Test.Features.Tracking
{
    public class TrackerTest
    {
        private Tracker _sytemUnderTest;
        
        [SetUp]
        public void Setup()
        {
            _sytemUnderTest = new Tracker();
        }

        [Test]
        public void Longitude_UpdateLocation_ReturnsUpdatedLongitude()
        {
            _sytemUnderTest.UpdateLocation(30.12, -32.531234, new DateTime(2021, 7, 12));
            Assert.That(_sytemUnderTest.Longitude, Is.EqualTo(30.12));
        }

        [Test]
        public void Latitude_UpdateLocation_ReturnsUpdatedLatitude()
        {
            _sytemUnderTest.UpdateLocation(30.12, -32.531234, new DateTime(2021, 7, 12));
            Assert.That(_sytemUnderTest.Latitude, Is.EqualTo(-32.531234));
        }

        [Test]
        public void LastUpdate_UpdateLocation_ReturnsUpdatedDateTime()
        {
            _sytemUnderTest.UpdateLocation(30.12, -32.531234, new DateTime(2021, 7, 12));
            Assert.That(_sytemUnderTest.LastUpdate, Is.EqualTo(new DateTime(2021, 7, 12)));
        }

        [Test]
        public void GetUncommittedEvents_AfterTwoUpdates_ReturnsTwoUncommittedEvents()
        {
            _sytemUnderTest.UpdateLocation(30.12, -32.531234, new DateTime(2021, 7, 12));
            _sytemUnderTest.UpdateLocation(30.50, -32.533214, new DateTime(2021, 7, 13));
            Assert.That(_sytemUnderTest.GetUncommittedEvents().Count, Is.EqualTo(2));
        }

        [Test]
        public void GetUncommittedEvents_AfterClearEvents_ReturnsZeroUncommittedEvents()
        {
            _sytemUnderTest.UpdateLocation(30.12, -32.531234, new DateTime(2021, 7, 12));
            _sytemUnderTest.ClearUncommittedEvents();
            Assert.That(_sytemUnderTest.GetUncommittedEvents().Count, Is.EqualTo(0));
        }
    }
}
