using System;

namespace Domain.Models
{
    public record ReturnedSpeaker(string SerialNumber, string Model, Guid RentalId, DateTime DateReturned);
}
