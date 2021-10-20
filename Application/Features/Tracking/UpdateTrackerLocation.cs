using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tracking
{
    public record UpdateTrackerLocationCommand : IRequest
    {
        public double Longitude { get; init; }

        public double Latitude { get; init; }

        public Guid TrackerId { get; init; }

        public DateTime TimeOfUpdate { get; init; }
    }

    public class UpdateTrackerLocationCommandHandler : IRequestHandler<UpdateTrackerLocationCommand>
    {
        public async Task<Unit> Handle(UpdateTrackerLocationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
