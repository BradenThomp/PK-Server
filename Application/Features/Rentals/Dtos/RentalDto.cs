using Application.Features.Speaker.Dtos;
using System;
using System.Collections.Generic;

namespace Application.Features.Rentals.Dtos
{
    public record RentalDto(Guid Id, DateTime RentalDate, DateTime ExpectedReturnDate, CustomerDto Customer, VenueDto venue, IEnumerable<SpeakerDto> Speakers);
}
