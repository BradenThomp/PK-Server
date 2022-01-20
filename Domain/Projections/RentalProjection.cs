using System;

namespace Domain.Projections
{
    public record RentalProjection(Guid CustomerId, DateTime RentalDate, DateTime ExpectedReturnDate, Guid VenueId, Guid Id) : IProjection;
}
