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

        /// <summary>
        /// Assigns a tracker to the given speaker if both the speaker and tracker are available.
        /// </summary>
        /// <param name="request">The command wrapper.</param>
        /// <param name="cancellationToken">Token to cancel the command.</param>
        /// <returns>Empty value if return is successful.</returns>
        public async Task<Unit> Handle(TrackSpeakerCommand request, CancellationToken cancellationToken)
        {
            var t = await _trackerRepo.GetAsync(request.TrackerId);
            var s = await _speakerRepo.GetAsync(request.SpeakerSerialNumber);
            s.AttachTracker(t);
            await _speakerRepo.UpdateAsync(s);
            return Unit.Value;
        }
    }
}
