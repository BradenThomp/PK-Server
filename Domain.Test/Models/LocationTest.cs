using Domain.Common.Exceptions;
using Domain.Models;
using NUnit.Framework;

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
            Assert.Throws<DomainValidationException>(() => new Location(-180.01, 90));
        }

        [Test]
        public void ShouldThrowValidationException_LongitudeNeg180AndLatidudeOver90_Constructor()
        {
            Assert.Throws<DomainValidationException>(() => new Location(-180, 90.01));
        }

        [Test]
        public void ShouldThrowValidationException_LongitudeOutOfExpectedRange()
        {
            var loc = new Location(0, 0);
            Assert.Throws<DomainValidationException>(() => loc.Longitude = 190);
        }

        [Test]
        public void ShouldThrowValidationException_LatitudeOutOfExpectedRange()
        {
            var loc = new Location(0, 0);
            Assert.Throws<DomainValidationException>(() => loc.Latitude = 100);
        }
    }
}
