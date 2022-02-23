using Application.Common.Notifications;
using System.Collections.Generic;

namespace Application.Features.Rentals.Notifications
{
    public record RentalReturnedNotification(IEnumerable<string> TrackerIds) : INotification;
}
