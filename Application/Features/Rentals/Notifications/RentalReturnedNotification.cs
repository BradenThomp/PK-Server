using Application.Common.Notifications;
using System.Collections.Generic;

namespace Application.Features.Rentals.Notifications
{
    /// <summary>
    /// A notification to be sent to clients when a rental is returned.
    /// Provides the client with a list of returned speaker serials so
    /// the speakers can be hidden from the map view.
    /// </summary>
    public record RentalReturnedNotification(IEnumerable<string> ReturnedSpeakerSerialNumbers) : INotification;
}
