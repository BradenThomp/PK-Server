using Application.Features.Speaker.Dtos;
using System;
using System.Collections.Generic;

namespace Application.Features.Rentals.Dtos
{
    public record RentalDto 
    {
        public Guid Id { get; init; }

        public DateTime RentalDate { get; init; }

        public DateTime ExpectedReturnDate { get; init; }

        public DateTime? DateReturned { get; init; }

        public CustomerDto Customer { get; init; }

        public VenueDto Destination { get; init; }

        public IEnumerable<RentedSpeakerDto> Speakers { get; init; }
    }
}
