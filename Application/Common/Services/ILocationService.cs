using Domain.Models;
using System.Threading.Tasks;

namespace Application.Common.Services
{
    /// <summary>
    /// A service for performing common location conversions.
    /// </summary>
    public interface ILocationService
    {
        /// <summary>
        /// Converts an address to geo-coordinators.
        /// </summary>
        /// <param name="address">The street address.</param>
        /// <param name="city">The city.</param>
        /// <param name="province">The province.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="country">The country.</param>
        /// <returns>The geo-location of the given address.</returns>
        public Task<Location> AddressToLocation(string address, string city, string province, string postalCode, string country);
    }
}
