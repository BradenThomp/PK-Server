using Application.Common.Repository;
using Domain.Aggregates;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tracking
{
    public record RegisterTrackerCommand(string MACAddress) : IRequest;

    public class RegisterTrackerCommandHandler : IRequestHandler<RegisterTrackerCommand>
    {
        private readonly IEventRepository _eventRepository;

        public RegisterTrackerCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Unit> Handle(RegisterTrackerCommand request, CancellationToken cancellationToken)
        {
            var aggregate = Tracker.RegisterTracker(request.MACAddress);
            await _eventRepository.SaveAsync(aggregate);
            return Unit.Value;
        }
    }
}
