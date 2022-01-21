using Domain.Common.Models;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Rental : IModel
    {
        public IEnumerable<Speaker> RentedSpeakers { get; set; }

        public Customer Customer { get; set; }

        public DateTime RentalDate { get; set; }

        public DateTime ExpectedReturnDate { get; set; }

        public Venue Destination { get; set; }

        public Guid Id { get; init; }
    }
}
