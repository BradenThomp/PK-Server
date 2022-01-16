using Application.Common.Repository;
using Domain.Aggregates;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Rentals
{
    public record CreateRentalCommand(IEnumerable<string> RentalSerialNumbers, Customer Customer, DateTime RentalDate, DateTime ExpectedReturnDate, Venue Destination) : IRequest;

    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly ISpeakerRepository _speakerRepository;

        public CreateRentalCommandHandler(IEventRepository eventRepository, ISpeakerRepository speakerRepository)
        {
            _eventRepository = eventRepository;
            _speakerRepository = speakerRepository;
        }

        public async Task<Unit> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            var speakers = new List<Domain.Models.Speaker>();
            foreach(var serial in request.RentalSerialNumbers)
            {
                speakers.Add(await _speakerRepository.GetAsync<string>(serial));
            }
            var aggregate = Rental.CreateRental(speakers, request.Customer, request.RentalDate, request.ExpectedReturnDate, request.Destination);
            await _eventRepository.SaveAsync(aggregate);
            return Unit.Value;
        }
    }
}
