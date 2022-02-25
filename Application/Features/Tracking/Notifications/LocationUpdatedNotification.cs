using Application.Common.Notifications;
using Application.Features.Tracking.Dtos;

namespace Application.Features.Tracking.Notifications
{
    public record LocationUpdatedNotification(string SpeakerSerialNumber, TrackerDto Tracker) : INotification;
}
