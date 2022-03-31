using Domain.Common.Exceptions;
using Domain.Common.Extensions;
using Domain.Common.Models;
using System;

namespace Domain.Models
{               
    /// <summary>
    /// Represents a location using longitude and latitude coordinates.
    /// </summary>
    public class Location : IModel
    {
        private double _longitude;
        public double Longitude { 
            get => _longitude; 
            set 
            { 
                if (value < -180 || value > 180)
                {
                    throw new DomainValidationException("Longitude must lie in range of -180 to 180 degrees.");
                }
                _longitude = value;
            } 
        }

        private double _latitude;
        public double Latitude
        {
            get => _latitude;
            set
            {
                if (value < -90 || value > 90)
                {
                    throw new DomainValidationException("Latitude must lie in range of -90 to 90 degrees.");
                }
                _latitude = value;
            }
        }

        public Guid Id { get; set; }

        public Location(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
            Id = Guid.NewGuid();
        }

        public Location(){}

        /// <summary>
        /// Returns true if this is within one kilometer of the other location.
        /// </summary>
        /// <param name="location">The other location.</param>
        /// <returns>True if the distance is within one kilometer.</returns>
        public bool WithinOneKilometer(Location location)
        {
            var distance = HaversineDistance(location, this);
            return distance < 1.0;
        }

        /// <summary>
        /// Checks if coordinates are placeholders (not valid).
        /// Set 0,0 as invalid as there will never be a venue in the middle of the ocean.
        /// </summary>
        /// <returns>True if placeholder, else false.</returns>
        public bool IsPlaceHolder() => Latitude == 0 && Longitude == 0;

        /// <summary>
        /// Returns the distance in kilometers between two geo-locations.
        /// </summary>
        /// <param name="pos1">Location 1</param>
        /// <param name="pos2">Location 2</param>
        /// <returns>Distance between two points in kilometers.</returns>
        private double HaversineDistance(Location pos1, Location pos2)
        {
            double radius = 6371;
            var lat = (pos2.Latitude - pos1.Latitude).ToRadians();
            var lng = (pos2.Longitude - pos1.Longitude).ToRadians();
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(pos1.Latitude.ToRadians()) * Math.Cos(pos2.Latitude.ToRadians()) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return radius * h2;
        }
    }
}
