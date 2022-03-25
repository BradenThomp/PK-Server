using Application.Common.Notifications;
using Application.Common.Repository;
using Application.Common.Services;
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
        private readonly ISpeakerRepository _speakerRepo;
        private readonly IRentalRepository _rentalRepo;
        private readonly INotificationService _notifications;
        private readonly IEmailService _emailService;

        public UpdateTrackerCommandHandler(ITrackerRepository repo, ISpeakerRepository speakerRepo, IRentalRepository rentalRepo, INotificationService notifications, IEmailService emailService)
        {
            _repo = repo;
            _notifications = notifications;
            _speakerRepo = speakerRepo;
            _rentalRepo = rentalRepo;
            _emailService = emailService;
        }

        /// <summary>
        /// Updates the tracker in the repository.
        /// </summary>
        /// <param name="request">The command wrapper.</param>
        /// <param name="cancellationToken">Token to cancel the command.</param>
        /// <returns>Empty value if return is successful.</returns>
        public async Task<Unit> Handle(UpdateTrackerCommand request, CancellationToken cancellationToken)
        {
            var t = await _repo.GetAsync(request.HardwareId);
            t.UpdateLocation(request.Longitude, request.Latitude);
            await _repo.UpdateAsync(t);
            if (!string.IsNullOrEmpty(t.SpeakerSerialNumber))
            {
                await _notifications.Notify(new LocationUpdatedNotification(t.SpeakerSerialNumber, new TrackerDto(t.HardwareId, t.LastUpdate, new LocationDto(t.Location.Longitude, t.Location.Latitude))));
                var s = await _speakerRepo.GetAsync(t.SpeakerSerialNumber);
                if (!s.ReachedDestination)
                {
                    var r = await _rentalRepo.GetAsync(s.RentalId);
                    if (!r.Destination.Cooridinates.IsPlaceHolder() && t.Location.Within250Meters(r.Destination.Cooridinates) && !s.ReachedDestination)
                    {
                        s.ReachedDestination = true;
                        await _speakerRepo.UpdateAsync(s);
                        await _emailService.MailAll("A rented speaker has reached it's destination.", $"Speaker {s.SerialNumber} has reached its destination at {r.Destination.Address}, {r.Destination.City}. This speaker was part of rental {s.RentalId}");
                    }
                }
            }
            return Unit.Value;
        }
    }
}
