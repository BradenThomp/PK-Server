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

        public Rental() { }

        public Rental(IEnumerable<Speaker> rentedSpeakers, Customer customer, DateTime rentalDate, DateTime expectedReturnDate, Venue destination)
        {
            RentedSpeakers = rentedSpeakers;
            Customer = customer;
            RentalDate = rentalDate;
            ExpectedReturnDate = expectedReturnDate;
            Destination = destination;
            Id = Guid.NewGuid();
        }
    }
}
