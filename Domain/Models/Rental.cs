using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Models
{
    public class Rental : IModel
    {
        public ICollection<Speaker> RentedSpeakers { get; set; }

        public ICollection<ReturnedSpeaker> ReturnedSpeakers { get; set; }

        public Customer Customer { get; set; }

        public DateTime RentalDate { get; set; }

        public DateTime? DateReturned { get; set; }

        public DateTime ExpectedReturnDate { get; set; }

        public Venue Destination { get; set; }

        public Guid Id { get; init; }

        public bool IsReturned => DateReturned != null;

        public Rental() { }

        public Rental(ICollection<Speaker> rentedSpeakers, Customer customer, DateTime rentalDate, DateTime expectedReturnDate, Venue destination)
        {
            RentedSpeakers = rentedSpeakers;
            Customer = customer;
            RentalDate = rentalDate;
            ExpectedReturnDate = expectedReturnDate;
            Destination = destination;
            Id = Guid.NewGuid();
            DateReturned = null;

            foreach(var rentedSpeaker in rentedSpeakers)
            {
                rentedSpeaker.RentalId = Id;
            }
        }

        public void ReturnSpeakers(IEnumerable<string> returnedSerialNumbers)
        {
            foreach(var serial in returnedSerialNumbers)
            {
                var speaker = RentedSpeakers.First(s => s.SerialNumber == serial);
                if (speaker is null)
                {
                    throw new InvalidOperationException($"Speaker being returned is not part of rental {Id}");
                }
                var returnedSpeaker = speaker.Return();
                RentedSpeakers.Remove(speaker);
                ReturnedSpeakers.Add(returnedSpeaker);
                if(RentedSpeakers.Count == 0)
                {
                    DateReturned = DateTime.UtcNow;
                }
            }
        }

    }
}
