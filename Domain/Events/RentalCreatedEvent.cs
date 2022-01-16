using Domain.Common.Events;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Domain.Events
{
    [Serializable]
    public record RentalCreatedEvent(IEnumerable<Speaker> RentedSpeakers, Customer Customer, DateTime RentalDate, DateTime ExpectedReturnDate, Venue Destination) : IEvent;
}
