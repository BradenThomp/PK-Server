using Application.Common.Notifications;
using Application.Common.Repository;
using Application.Features.Tracking.Dtos;
using Application.Features.Tracking.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tracking.Commands
{
    public record UpdateTrackerCommand(string HardwareId, double Longitude, double Latitude) : IRequest;

    public class UpdateTrackerCommandHandler : IRequestHandler<UpdateTrackerCommand>
    {
        private readonly ITrackerRepository _repo;

        private readonly INotificationService _notifications;

        public UpdateTrackerCommandHandler(ITrackerRepository repo, INotificationService notifications)
        {
            _repo = repo;
            _notifications = notifications;
        }

        public async Task<Unit> Handle(UpdateTrackerCommand request, CancellationToken cancellationToken)
        {
            var t = await _repo.GetAsync(request.HardwareId);
            t.UpdateLocation(request.Longitude, request.Latitude);
            await _repo.UpdateAsync(t);
            await _notifications.Notify(new LocationUpdatedNotification(new TrackerDto(t.HardwareId, t.LastUpdate, new LocationDto(t.Location.Longitude, t.Location.Latitude))));
            return Unit.Value;
        }
    }
}
