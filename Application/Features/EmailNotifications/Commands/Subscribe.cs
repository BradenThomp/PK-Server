using Application.Common.Repository;
using Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.EmailNotifications.Commands
{
    public record SubscribeCommand(string Email) : IRequest;

    public class SubscribeCommandHandler : IRequestHandler<SubscribeCommand>
    {
        private readonly INotificationEmailRepository _repo;

        public SubscribeCommandHandler(INotificationEmailRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Adds a new email that subscribes to notifications to the repository.
        /// </summary>
        /// <param name="request">The command wrapper.</param>
        /// <param name="cancellationToken">Token to cancel the command.</param>
        /// <returns>Empty value if return is successful.</returns>
        public async Task<Unit> Handle(SubscribeCommand request, CancellationToken cancellationToken)
        {
            var email = new NotificationEmail()
            {
                Email = request.Email
        };
            await _repo.AddAsync(email);
            return Unit.Value;
        }
    }
}
