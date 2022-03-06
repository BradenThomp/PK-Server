using Application.Common.Notifications;
using Application.Common.Repository;
using Application.Features.Rentals.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Commands
{
    public record ReturnRentalCommand(Guid RentalId, IEnumerable<string> ReturnedSpeakerSerialNumbers) : IRequest;

    public class ReturnRentalCommandHandler : IRequestHandler<ReturnRentalCommand, Unit>
    {
        private readonly IRentalRepository _rentalRepo;

        private readonly INotificationService _notificationService;

        public ReturnRentalCommandHandler(IRentalRepository rentalRepo, INotificationService notificationService)
        {
            _rentalRepo = rentalRepo;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Returns speakers of a rental. Does not have to return all speakers, the return can be partial.
        /// </summary>
        /// <param name="request">The command wrapper.</param>
        /// <param name="cancellationToken">Token to cancel the command.</param>
        /// <returns>Empty value if return is successful.</returns>
        public async Task<Unit> Handle(ReturnRentalCommand request, CancellationToken cancellationToken)
        {
            var rental = await _rentalRepo.GetAsync(request.RentalId);
            rental.ReturnSpeakers(request.ReturnedSpeakerSerialNumbers);
            await _rentalRepo.UpdateAsync(rental);
            await _notificationService.Notify(new RentalReturnedNotification(request.ReturnedSpeakerSerialNumbers));
            return Unit.Value;
        }
    }
}
