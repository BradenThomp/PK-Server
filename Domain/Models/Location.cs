using Domain.Common.Models;
using System;

namespace Domain.Models
{               
    public class Location : IModel
    {
        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public Location(double longitude, double latitude)
        {
            if (longitude < -180 || longitude > 180)
            {
                throw new InvalidOperationException("Longitude must lie in range of -180 to 180 degrees.");
            }
            if (latitude < -90 || latitude > 90)
            {
                throw new InvalidOperationException("Latitude must lie in range of -90 to 90 degrees.");
            }
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
