using Application.Common.Repository;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Features.Speaker.Commands
{
    public record AddSpeakerCommand(string Model, string SerialNumber) : IRequest;

    public class AddSpeakerCommandHandler : IRequestHandler<AddSpeakerCommand>
    {
        private readonly ISpeakerRepository _repository;

        public AddSpeakerCommandHandler(ISpeakerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddSpeakerCommand request, CancellationToken cancellationToken)
        {
            //var speaker = new Domain.Models.Speaker(request.SerialNumber, request.Model);
            //await _repository.AddAsync(speaker);
            return Unit.Value;
        }
    }
}
