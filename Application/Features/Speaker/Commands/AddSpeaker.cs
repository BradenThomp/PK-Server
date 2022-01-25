using Application.Common.Repository;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Features.Speaker.Commands
{
    public record AddSpeakerCommand(string Model, string SerialNumber) : IRequest;

    public class AddSpeakerCommandHandler : IRequestHandler<AddSpeakerCommand>
    {
        private readonly ISpeakerRepository _repo;

        public AddSpeakerCommandHandler(ISpeakerRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(AddSpeakerCommand request, CancellationToken cancellationToken)
        {
            var s = new Domain.Models.Speaker(request.SerialNumber, request.Model);
            await _repo.AddAsync(s);
            return Unit.Value;
        }
    }
}
