using Application.Common.Notifications;
using Application.Features.Map.Dtos;
using System.Collections.Generic;

namespace Application.Features.Rentals.Notifications
{
    public record RentalCreatedNotification(IEnumerable<MapPlotPointDto> MapPlotPoints) : INotification;
}
