using Domain.Common.Models;
using System;

namespace Domain.Models
{
    /// <summary>
    /// Represents a venue that is expecting a rental.
    /// </summary>
    public class Venue : IModel
    {
        public Location Cooridinates { get; set; }

        public Guid Id { get; init; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string PostalCode { get; set; }

        public Venue() { }

        public Venue(Location coordinates, string address, string city, string province, string postalCode)
        {
            Cooridinates = coordinates;
            Address = address;
            City = city;
            Province = province;
            PostalCode = postalCode;
            Id = Guid.NewGuid();
        }
    }
}
