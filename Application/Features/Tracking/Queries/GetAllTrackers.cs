using Application.Common.Repository;
using Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tracking.Queries
{
    public record GetAllTrackersQuery() : IRequest<IEnumerable<Tracker>>;

    public class GetAllTrackersQueryHandler : IRequestHandler<GetAllTrackersQuery, IEnumerable<Tracker>>
    {
        private readonly ITrackerRepository _repo;

        public GetAllTrackersQueryHandler(ITrackerRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Tracker>> Handle(GetAllTrackersQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync();
        }
    }
}