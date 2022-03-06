using System;
using System.Collections.Generic;

namespace Application.Features.Rentals.Dtos
{
    /// <summary>
    /// A Data transfer object for returning a rental.
    /// Uses the old record declaration format because automapper
    /// resolvers do not work with the shortcut declaration.
    /// </summary>
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
