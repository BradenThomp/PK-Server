using Application.Common.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tracking.Commands
{
    public record UpdateTrackerCommand(string HardwareId, double Longitude, double Latitude) : IRequest;

    public class UpdateTrackerCommandHandler : IRequestHandler<UpdateTrackerCommand>
    {
        private readonly ITrackerRepository _repo;

        public UpdateTrackerCommandHandler(ITrackerRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(UpdateTrackerCommand request, CancellationToken cancellationToken)
        {
            var tracker = await _repo.GetAsync(request.HardwareId);
            tracker.UpdateLocation(request.Longitude, request.Latitude);
            await _repo.UpdateAsync(tracker);
            return Unit.Value;
        }
    }
}
