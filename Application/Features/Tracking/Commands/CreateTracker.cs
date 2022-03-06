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

        /// <summary>
        /// Adds a new tracker to the repository.
        /// </summary>
        /// <param name="request">The command wrapper.</param>
        /// <param name="cancellationToken">Token to cancel the command.</param>
        /// <returns>Empty value if return is successful.</returns>
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
