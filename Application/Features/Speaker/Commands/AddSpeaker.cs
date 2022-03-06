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

        /// <summary>
        /// Adds a speaker to the repository.
        /// </summary>
        /// <param name="request">The command wrapper.</param>
        /// <param name="cancellationToken">Token to cancel the command.</param>
        /// <returns>Empty value if return is successful.</returns>
        public async Task<Unit> Handle(AddSpeakerCommand request, CancellationToken cancellationToken)
        {
            var s = new Domain.Models.Speaker(request.SerialNumber, request.Model);
            await _repo.AddAsync(s);
            return Unit.Value;
        }
    }
}
