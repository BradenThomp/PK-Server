using Application.Common.Repository;
using Application.Features.Rentals.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Commands
{
    public record CreateRentalCommand(IEnumerable<SpeakerTrackerMappingDto> SpeakerTrackerMappings, CustomerDto Customer, VenueDto Destination, DateTime RentalDate, DateTime ExpectedReturnDate) : IRequest<Guid>;

    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Guid>
    {
        private readonly ITrackerRepository _trackerRepo;
        private readonly ISpeakerRepository _speakerRepo;
        private readonly IRentalRepository _rentalRepo;

        public CreateRentalCommandHandler(IRentalRepository rentalRepo, ISpeakerRepository speakerRepo, ITrackerRepository trackerRepo)
        {
            _rentalRepo = rentalRepo;
            _speakerRepo = speakerRepo;
            _trackerRepo = trackerRepo;
        }

        public async Task<Guid> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            var rentedSpeakers = new List<Domain.Models.Speaker>();
            foreach(var m in request.SpeakerTrackerMappings)
            {
                var t = await _trackerRepo.GetAsync(m.TrackerHardwareId);
                var s = await _speakerRepo.GetAsync(m.SpeakerSerialNumber);
                s.Tracker = t;
                rentedSpeakers.Add(s);
            }
            var d = request.Destination;
            var destination = new Domain.Models.Venue(d.Address, d.City, d.Province, d.PostalCode);
            var c = request.Customer;
            var customer = new Domain.Models.Customer(c.Name, c.Phone, c.Email);
            var rental = new Domain.Models.Rental(rentedSpeakers, customer, request.RentalDate, request.ExpectedReturnDate, destination);
            await _rentalRepo.AddAsync(rental);
            return rental.Id;
        }
    }
}
