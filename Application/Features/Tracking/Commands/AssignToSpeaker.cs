using Application.Common.Repository;
using Domain.Aggregates;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tracking.Commands
{
    public record AssignToSpeakerCommand(string TrackerId, string SpeakerSerialNumber) : IRequest;

    public class AssignToSpeakerCommandHandler : IRequestHandler<AssignToSpeakerCommand>
    {
        private readonly IEventRepository _eventRepository;

        private readonly ISpeakerRepository _speakerRepository;

        public AssignToSpeakerCommandHandler(IEventRepository eventRepository, ISpeakerRepository speakerRepository)
        {
            _eventRepository = eventRepository;
            _speakerRepository = speakerRepository;
        }

        public async Task<Unit> Handle(AssignToSpeakerCommand request, CancellationToken cancellationToken)
        {
            var tracker = await _eventRepository.GetByIdAsync<Tracker>(request.TrackerId);
            var speaker = await _speakerRepository.GetAsync<string>(request.SpeakerSerialNumber);
            tracker.AssignSpeaker(speaker);
            await _eventRepository.SaveAsync(tracker);
            await _speakerRepository.UpdateAsync(speaker);
            return Unit.Value;
        }
    }
}
