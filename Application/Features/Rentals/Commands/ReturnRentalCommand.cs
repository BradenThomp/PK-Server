using Application.Common.Repository;
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

        public ReturnRentalCommandHandler(IRentalRepository rentalRepo)
        {
            _rentalRepo = rentalRepo;
        }

        public async Task<Unit> Handle(ReturnRentalCommand request, CancellationToken cancellationToken)
        {
            var rental = await _rentalRepo.GetAsync(request.RentalId);
            rental.ReturnSpeakers(request.ReturnedSpeakerSerialNumbers);
            await _rentalRepo.UpdateAsync(rental);
            return Unit.Value;
        }
    }
}
