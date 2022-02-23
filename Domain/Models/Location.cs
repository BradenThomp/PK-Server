using Domain.Common.Exceptions;
using Domain.Common.Models;
using System;

namespace Domain.Models
{               
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
    }
}
