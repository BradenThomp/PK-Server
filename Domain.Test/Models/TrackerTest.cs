using Domain.Models;
using NUnit.Framework;

namespace Domain.Test.Models
{
    public class TrackerTest
    {
        [Test]
        public void ShouldUpdateLocationAndTime_ValidLocation_UpdateLocation()
        {
            Tracker t = new Tracker()
            {
                HardwareId = "1f3g554k",
                Location = new Location(0, 0),
                LastUpdate = new System.DateTime()
            };
            Assert.That(t.Location.Latitude, Is.EqualTo(0));
            Assert.That(t.Location.Longitude, Is.EqualTo(0));
            Assert.That(t.LastUpdate, Is.EqualTo(new System.DateTime()));
            t.UpdateLocation(40, -70);
            Assert.That(t.Location.Latitude, Is.EqualTo(-70));
            Assert.That(t.Location.Longitude, Is.EqualTo(40));
            Assert.That(t.LastUpdate, Is.Not.EqualTo(new System.DateTime()));
        }
    }
}
