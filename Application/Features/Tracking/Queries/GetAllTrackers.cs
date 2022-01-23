using Application.Common.Repository;
using Application.Features.Tracking.Dtos;
using Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tracking.Queries
{
    public record GetAllTrackersQuery() : IRequest<IEnumerable<TrackerDto>>;

    public class GetAllTrackersQueryHandler : IRequestHandler<GetAllTrackersQuery, IEnumerable<TrackerDto>>
    {
        private readonly ITrackerRepository _repo;

        public GetAllTrackersQueryHandler(ITrackerRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TrackerDto>> Handle(GetAllTrackersQuery request, CancellationToken cancellationToken)
        {
            var trackers = await _repo.GetAllAsync();
            return trackers.Select(t => new TrackerDto(t.HardwareId, t.LastUpdate, new LocationDto(t.Location.Longitude, t.Location.Latitude)));
        }
    }
}