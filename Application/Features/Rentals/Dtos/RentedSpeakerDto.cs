using Application.Features.Tracking.Dtos;
using System;

namespace Application.Features.Rentals.Dtos
{
    /// <summary>
    /// A Data transfer object that represents a rented speaker.
    /// </summary>
    public record RentedSpeakerDto(string SerialNumber, string Model, TrackerDto Tracker, DateTime? DateReturned);
}
