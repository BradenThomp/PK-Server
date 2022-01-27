using Application.Features.Speaker.Dtos;
using System;
using System.Collections.Generic;

namespace Application.Features.Rentals.Dtos
{
    public record RentalDto(Guid Id, DateTime RentalDate, DateTime ExpectedReturnDate, DateTime DateReturned, CustomerDto Customer, VenueDto Venue, IEnumerable<SpeakerDto> RentedSpeakers, IEnumerable<ReturnedSpeakerDto> ReturnedSpeakers);
}
