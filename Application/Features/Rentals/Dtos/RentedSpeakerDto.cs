using Application.Features.Tracking.Dtos;
using System;

namespace Application.Features.Rentals.Dtos
{
    public record RentedSpeakerDto(string SerialNumber, string Model, TrackerDto Tracker, DateTime? DateReturned);
}
