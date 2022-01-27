using System;

namespace Application.Features.Rentals.Dtos
{
    public record ReturnedSpeakerDto(string SerialNumber, string Model, DateTime DateReturned);
}
