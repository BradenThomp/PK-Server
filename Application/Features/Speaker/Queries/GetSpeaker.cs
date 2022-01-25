using Application.Common.Repository;
using Application.Features.Speaker.Dtos;
using Application.Features.Tracking.Dtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Speaker.Queries
{
    public record GetSpeakerQuery(string SerialNumber) : IRequest<SpeakerDto>;

    public class GetSpeakerQueryQueryHandler : IRequestHandler<GetSpeakerQuery, SpeakerDto>
    {
        private readonly ISpeakerRepository _repository;

        public GetSpeakerQueryQueryHandler(ISpeakerRepository repository)
        {
            _repository = repository;
        }

        public async Task<SpeakerDto> Handle(GetSpeakerQuery request, CancellationToken cancellationToken)
        {
            var s = await _repository.GetAsync(request.SerialNumber);
            var t = s.Tracker;
            var trackerDto = (TrackerDto)null;
            if (t is not null)
            {
                trackerDto = new TrackerDto(t.HardwareId, t.LastUpdate, new LocationDto(t.Location.Longitude, t.Location.Latitude));
            }
            return new SpeakerDto(s.Model, s.SerialNumber, trackerDto);
        }
    }
}
