using Application.Common.Notifications;
using Application.Features.Map.Dtos;
using System.Collections.Generic;

namespace Application.Features.Rentals.Notifications
{
    /// <summary>
    /// A notification to be sent to clients when a rental is created.
    /// Returns of list of new map plot points so the rented speakers can be
    /// displayed in the clients map view.
    /// </summary>
    public record RentalCreatedNotification(IEnumerable<MapPlotPointDto> MapPlotPoints) : INotification;
}
