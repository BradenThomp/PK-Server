using Domain.Common.Aggregates;
using Domain.Events;
using Domain.Models;
using Domain.Projections;
using System;
using System.Collections.Generic;

namespace Domain.Aggregates
{
    public class Rental : AggregateBase
    {
        public IEnumerable<Speaker> RentedSpeakers { get; private set; }

        public Customer Customer { get; private set; }

        public DateTime RentalDate { get; private set; }

        public DateTime ExpectedReturnDate { get; private set; }

        public Venue Destination { get; private set; }

        private Guid RentalId { get; set; }

        public override string Id { get => RentalId.ToString(); }

        /// <summary>
        /// Constructor used for rebuilding aggregate from event history. This should not be used to register new Rental objects for the system.
        /// </summary>
        private Rental() { }
        private Rental(IEnumerable<Speaker> rentedSpeakers, Customer customer, DateTime rentalDate, DateTime expectedReturnDate, Venue destination)
        {
            Raise(new RentalCreatedEvent(rentedSpeakers, customer, rentalDate, expectedReturnDate, destination));
        }

        public static Rental CreateRental(IEnumerable<Speaker> rentedSpeakers, Customer customer, DateTime rentalDate, DateTime expectedReturnDate, Venue destination)
        {
            return new Rental(rentedSpeakers, customer, rentalDate, expectedReturnDate, destination);
        }

        private void Apply(RentalCreatedEvent @event)
        {
            RentedSpeakers = @event.RentedSpeakers;
            Customer = @event.Customer;
            RentalDate = @event.RentalDate;
            ExpectedReturnDate = @event.ExpectedReturnDate;
            Destination = @event.Destination;
            RentalId = Guid.NewGuid();
        }

        public override IProjection CreateProjection()
        {
            throw new NotImplementedException();
        }
    }
}
