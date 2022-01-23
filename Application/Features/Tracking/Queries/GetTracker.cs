using Application.Common.Repository;
using Application.Features.Tracking.Dtos;
using Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tracking.Queries
{
    public record GetTrackerQuery(string HardwareId) : IRequest<TrackerDto>;

    public class GetTrackerQueryHandler : IRequestHandler<GetTrackerQuery, TrackerDto>
    {
        private readonly ITrackerRepository _repo;

        public GetTrackerQueryHandler(ITrackerRepository repo)
        {
            _repo = repo;
        }

        public async Task<TrackerDto> Handle(GetTrackerQuery request, CancellationToken cancellationToken)
        {
            var t = await _repo.GetAsync(request.HardwareId);
            return new TrackerDto(t.HardwareId, t.LastUpdate, new LocationDto(t.Location.Longitude, t.Location.Latitude));
        }
    }
}
