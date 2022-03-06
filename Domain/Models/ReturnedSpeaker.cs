using System;

namespace Domain.Models
{
    /// <summary>
    /// Represents a speaker that has been returned. For history keeping purposes, the record is unmodifyable.
    /// </summary>
    public record ReturnedSpeaker(string SerialNumber, string Model, Guid RentalId, DateTime DateReturned);
}
