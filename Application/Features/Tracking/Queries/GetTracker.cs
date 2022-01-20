using Application.Common.Repository;
using Application.Features.Speaker.Dtos;
using Application.Features.Tracking.Dtos;
using Domain.Aggregates;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tracking
{
    public record GetTrackerQuery(string MACAddress) : IRequest<TrackerDto>;

    public class RegisterTrackerQueryHandler : IRequestHandler<GetTrackerQuery, TrackerDto>
    {
        private readonly IEventRepository _eventRepository;

        public RegisterTrackerQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<TrackerDto> Handle(GetTrackerQuery request, CancellationToken cancellationToken)
        {
            var tracker = await _eventRepository.GetByIdAsync<Tracker>(request.MACAddress);
            var speakerDto = tracker.AssignedSpeaker is null ? null : new SpeakerDto(tracker.AssignedSpeaker.Model, tracker.AssignedSpeaker.SerialNumber);
            return new TrackerDto(tracker.Id, tracker.Location.Longitude, tracker.Location.Latitude, tracker.LastUpdate, speakerDto);
        }
    }
}
