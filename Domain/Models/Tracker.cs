using Domain.Common.Models;
using System;

namespace Domain.Models
{
    /// <summary>
    /// A tracker used to track the location of speakers.
    /// </summary>
    public class Tracker : IModel
    {
        public string HardwareId { get; init; }

        public DateTime LastUpdate { get; set; }

        public Location Location { get; set; }

        public string SpeakerSerialNumber { get; set; }

        /// <summary>
        /// Updates the location with the given coordinates.
        /// </summary>
        /// <param name="longitude">The new longitude coordinates.</param>
        /// <param name="latitude">The new latitude coordinates.</param>
        public void UpdateLocation(double longitude, double latitude)
        {
            Location.Longitude = longitude;
            Location.Latitude = latitude;
            LastUpdate = DateTime.UtcNow;
        }
    }
}
