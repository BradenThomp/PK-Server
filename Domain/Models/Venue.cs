using Domain.Common.Models;
using System;

namespace Domain.Models
{
    public class Venue : IModel
    {
        //public Location Cooridinates { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string PostalCode { get; set; }

        public Guid Id { get; init; }
    }
}
