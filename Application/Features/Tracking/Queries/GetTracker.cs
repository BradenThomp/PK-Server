using Application.Common.Repository;
using Application.Features.Tracking.Dtos;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tracking.Queries
{
    public record GetTrackerQuery(string HardwareId) : IRequest<TrackerDto>;

    public class GetTrackerQueryHandler : IRequestHandler<GetTrackerQuery, TrackerDto>
    {
        private readonly ITrackerRepository _repo;
        private readonly IMapper _mapper;

        public GetTrackerQueryHandler(ITrackerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a tracker with the given id.
        /// </summary>
        /// <param name="request">The query wrapper.</param>
        /// <param name="cancellationToken">Token to cancel the task.</param>
        /// <returns>The tracker with the given id.</returns>
        public async Task<TrackerDto> Handle(GetTrackerQuery request, CancellationToken cancellationToken)
        {
            var t = await _repo.GetAsync(request.HardwareId);
            var result = _mapper.Map<TrackerDto>(t);
            return result;
        }
    }
}
