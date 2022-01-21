using Application.Common.Repository;
using Application.Features.Customer.Dtos;
using Application.Features.Venue.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Rentals
{
    //public record CreateRentalCommand(IEnumerable<AssignToSpeakerCommand> SpeakerTrackerAssignments, CreateCustomerDto Customer, CreateVenueDto Destination, DateTime RentalDate, DateTime ExpectedReturnDate) : IRequest;

    /**public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly ISpeakerRepository _speakerRepository;
        private readonly IMediator _mediator;

        public CreateRentalCommandHandler(IEventRepository eventRepository, ISpeakerRepository speakerRepository, IMediator mediator)
        {
            _eventRepository = eventRepository;
            _speakerRepository = speakerRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            var speakers = new List<Domain.Models.Speaker>();
            foreach(var cmd in request.SpeakerTrackerAssignments)
            {
                await _mediator.Send(cmd);
                speakers.Add(await _speakerRepository.GetAsync(cmd.SpeakerSerialNumber));
            }
            var c = request.Customer;
            var customer = new Domain.Models.Customer(c.Name, c.Phone, c.Email);
            var d = request.Destination;
            var destination = new Domain.Models.Venue(d.Address, d.City, d.Province, d.PostalCode);
            var aggregate = Rental.CreateRental(speakers, customer, request.RentalDate, request.ExpectedReturnDate, destination);
            await _eventRepository.SaveAsync(aggregate);
            return Unit.Value;
        }**/
    //}
}
