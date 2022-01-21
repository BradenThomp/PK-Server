using Application.Common.Repository;
using Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tracking.Commands
{
    public record CreateTrackerCommand(string HardwareId, double Longitude, double Latitude) : IRequest;

    public class CreateTrackerCommandHandler : IRequestHandler<CreateTrackerCommand>
    {
        private readonly ITrackerRepository _repo;

        public CreateTrackerCommandHandler(ITrackerRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(CreateTrackerCommand request, CancellationToken cancellationToken)
        {
            var tracker = new Tracker
            {
                Location = new Location(request.Longitude, request.Latitude),
                HardwareId = request.HardwareId,
                LastUpdate = DateTime.UtcNow
            };
            await _repo.AddAsync(tracker);
            return Unit.Value;
        }
    }
}
