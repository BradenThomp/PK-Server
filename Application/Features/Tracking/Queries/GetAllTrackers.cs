using Application.Common.Repository;
using Application.Features.Tracking.Dtos;
using AutoMapper;
using Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tracking.Queries
{
    public record GetAllTrackersQuery() : IRequest<IEnumerable<TrackerDto>>;

    public class GetAllTrackersQueryHandler : IRequestHandler<GetAllTrackersQuery, IEnumerable<TrackerDto>>
    {
        private readonly ITrackerRepository _repo;
        private readonly IMapper _mapper;

        public GetAllTrackersQueryHandler(ITrackerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrackerDto>> Handle(GetAllTrackersQuery request, CancellationToken cancellationToken)
        {
            var trackers = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<Tracker>, IEnumerable<TrackerDto>>(trackers);
        }
    }
}