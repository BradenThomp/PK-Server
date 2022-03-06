using Application.Common.Notifications;
using Application.Features.Tracking.Dtos;

namespace Application.Features.Tracking.Notifications
{
    /// <summary>
    /// A notification sent to clients when a tracker location is updated.
    /// Used to update the clients map view.
    /// </summary>
    public record LocationUpdatedNotification(string SpeakerSerialNumber, TrackerDto Tracker) : INotification;
}
