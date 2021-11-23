using Domain.Models;
using NUnit.Framework;
using System;

namespace Domain.Test.Models
{
    public class LocationTest
    {
        [Test]
        public void ShouldConstructObject_LongitudeNeg180AndLatidude90()
        {
            Location location = new Location(-180, 90);
            Assert.That(location.Longitude, Is.EqualTo(-180));
            Assert.That(location.Latitude, Is.EqualTo(90));
        }

        [Test]
        public void ShouldThrowValidationException_LongitudeUnderNeg180AndLatidude90_Constructor()
        {
            Assert.Throws<InvalidOperationException>(() => new Location(-180.01, 90));
        }

        [Test]
        public void ShouldThrowValidationException_LongitudeNeg180AndLatidudeOver90_Constructor()
        {
            Assert.Throws<InvalidOperationException>(() => new Location(-180, 90.01));
        }
    }
}
