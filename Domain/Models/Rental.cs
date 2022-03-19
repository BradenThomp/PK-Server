using Domain.Common.Exceptions;
using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Models
{
    /// <summary>
    /// Represents a customer's rental of speakers.
    /// </summary>
    public class Rental : IModel
    {
        /// <summary>
        /// A collection of speakers that the customer has currently out for rental.
        /// </summary>
        public ICollection<Speaker> RentedSpeakers { get; set; }

        /// <summary>
        /// A collection of speakers that have been returned by the customer.
        /// </summary>
        public ICollection<ReturnedSpeaker> ReturnedSpeakers { get; set; }

        /// <summary>
        /// The customer who is responsible for the rental.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// The date that the rental occured.
        /// </summary>
        public DateTime RentalDate { get; set; }

        /// <summary>
        /// The date that the entire rental was returned on.
        /// This means all speakers have to be returned for 
        /// the entire rental to count as returned.
        /// </summary>
        public DateTime? DateReturned { get; set; }

        /// <summary>
        /// The date that all speakers are expected to be returned by.
        /// </summary>
        public DateTime ExpectedReturnDate { get; set; }

        /// <summary>
        /// The venue that the speakers are to be used at.
        /// </summary>
        public Venue Destination { get; set; }

        /// <summary>
        /// The rental id.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Returns true if all speakers have been returned.
        /// </summary>
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

        /// <summary>
        /// Marks the provided speakers as returned.
        /// Marks the entire rental as returned if all speakers are returned.
        /// </summary>
        /// <param name="returnedSerialNumbers">The list of returned speakers.</param>
        public void ReturnSpeakers(IEnumerable<string> returnedSerialNumbers)
        {
            foreach(var serial in returnedSerialNumbers)
            {
                var speaker = RentedSpeakers.FirstOrDefault(s => s.SerialNumber == serial);
                if (speaker is null)
                {
                    throw new DomainValidationException($"Speaker being returned is not part of rental or has already been returned {Id}");
                }
                var returnedSpeaker = speaker.Return();
                RentedSpeakers.Remove(speaker);
                if(ReturnedSpeakers is null)
                {
                    ReturnedSpeakers = new List<ReturnedSpeaker>();
                }
                ReturnedSpeakers.Add(returnedSpeaker);
                if(RentedSpeakers.Count == 0)
                {
                    DateReturned = DateTime.UtcNow;
                }
            }
        }

    }
}
