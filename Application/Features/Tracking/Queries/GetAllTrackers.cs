using Application.Common.Repository;
using Application.Features.Speaker.Dtos;
using Application.Features.Tracking.Dtos;
using Domain.Aggregates;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tracking
{
    public record GetAllTrackersQuery() : IRequest<IEnumerable<TrackerDto>>;

    public class GetAllTrackersQueryHandler : IRequestHandler<GetAllTrackersQuery, IEnumerable<TrackerDto>>
    {
        private readonly ITrackerProjectionRepository _repository;

        private readonly ISpeakerRepository _speakerRepository;

        public GetAllTrackersQueryHandler(ITrackerProjectionRepository repository, ISpeakerRepository speakerRepository)
        {
            _repository = repository;
            _speakerRepository = speakerRepository;
        }

        public async Task<IEnumerable<TrackerDto>> Handle(GetAllTrackersQuery request, CancellationToken cancellationToken)
        {
            var trackerProjections = await _repository.GetAllAsync();
            return trackerProjections.Select(t => new TrackerDto(t.MACAddress, t.Longitude, t.Latitude, t.LastUpdate, new SpeakerDto(t.Speaker.Model, t.Speaker.SerialNumber)));
        }
    }
}