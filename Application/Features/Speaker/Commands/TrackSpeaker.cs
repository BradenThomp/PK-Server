using Application.Common.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Speaker.Commands
{
    public record TrackSpeakerCommand(string SpeakerSerialNumber, string TrackerId) : IRequest;

    public class TrackSpeakerCommandHandler : IRequestHandler<TrackSpeakerCommand>
    {
        private readonly ISpeakerRepository _speakerRepo;

        private readonly ITrackerRepository _trackerRepo;

        public TrackSpeakerCommandHandler(ISpeakerRepository speakerRepo, ITrackerRepository trackerRepo)
        {
            _speakerRepo = speakerRepo;
            _trackerRepo = trackerRepo;
        }

        public async Task<Unit> Handle(TrackSpeakerCommand request, CancellationToken cancellationToken)
        {
            var t = await _trackerRepo.GetAsync(request.TrackerId);
            var s = await _speakerRepo.GetAsync(request.SpeakerSerialNumber);
            s.Tracker = t;
            await _speakerRepo.UpdateAsync(s);
            return Unit.Value;
        }
    }
}
