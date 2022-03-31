using Application.Common.Services;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Infrastructure.Test.Services
{
    public class LocationServiceTest
    {
        [Test]
        public async Task ShouldReturnGeoCoordinates_SaddleDomeAddressProvided_AddressToLocation()
        {
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c.GetConnectionString(It.IsAny<string>())).Returns("ZGQomU1fmfAWUjfYWgEHp9IQwrvpeSsm");
            ILocationService locationService = new LocationService(configMock.Object);
            var location = await locationService.AddressToLocation("555 Saddledome Rise SE", "Calgary", "Alberta", "T2G2W1", "Canada");
            Assert.That(location.Longitude, Is.EqualTo(-114.051964));
            Assert.That(location.Latitude, Is.EqualTo(51.037412));
        }

        [Test]
        public async Task ShouldReturnNull_InvalidCoordinatesProvided_AddressToLocation()
        {
            //ILocationService locationService = new LocationService();
            //var location = await locationService.AddressToLocation("W", "EEGds", "Albegewarta", "T2G2W1", "fe");
            //Assert.That(location, Is.Null);
        }
    }
}
