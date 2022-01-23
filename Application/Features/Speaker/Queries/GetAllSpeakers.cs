using Application.Common.Repository;
using Application.Features.Speaker.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Tracking.Dtos;

namespace Application.Features.Speaker.Queries
{
    public record GetAllSpeakersQuery() : IRequest<IEnumerable<SpeakerDto>>;

    public class GetAllSpeakersQueryHandler : IRequestHandler<GetAllSpeakersQuery, IEnumerable<SpeakerDto>>
    {
        private readonly ISpeakerRepository _repository;

        public GetAllSpeakersQueryHandler(ISpeakerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SpeakerDto>> Handle(GetAllSpeakersQuery request, CancellationToken cancellationToken)
        {
            var speakers = await _repository.GetAllAsync();
            var result = new List<SpeakerDto>();
            foreach(var s in speakers)
            {
                var trackerDto = (TrackerDto)null;
                var t = s.Tracker;
                if(t is not null)
                {
                    trackerDto = new TrackerDto(t.HardwareId, t.LastUpdate, new LocationDto(t.Location.Longitude, t.Location.Latitude));
                }
                result.Add(new SpeakerDto(s.Model, s.SerialNumber, trackerDto));
            }
            return result;
        }
    }
}
