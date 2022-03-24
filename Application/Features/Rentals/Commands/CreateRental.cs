using Application.Common.Notifications;
using Application.Common.Repository;
using Application.Common.Services;
using Application.Features.Map.Dtos;
using Application.Features.Rentals.Dtos;
using Application.Features.Rentals.Notifications;
using Application.Features.Tracking.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Commands
{
    public record CreateRentalCommand(IEnumerable<SpeakerTrackerMappingDto> SpeakerTrackerMappings, CustomerDto Customer, VenueDto Destination, DateTime RentalDate, DateTime ArrivalDate, DateTime ExpectedReturnDate) : IRequest<Guid>;

    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Guid>
    {
        private readonly ITrackerRepository _trackerRepo;
        private readonly ISpeakerRepository _speakerRepo;
        private readonly IRentalRepository _rentalRepo;
        private readonly INotificationService _notificationService;
        private readonly ILocationService _locationService;

        public CreateRentalCommandHandler(IRentalRepository rentalRepo, ISpeakerRepository speakerRepo, ITrackerRepository trackerRepo, INotificationService notificationService, ILocationService locationService)
        {
            _rentalRepo = rentalRepo;
            _speakerRepo = speakerRepo;
            _trackerRepo = trackerRepo;
            _notificationService = notificationService;
            _locationService = locationService;
        }

        /// <summary>
        /// Creates a new rental and saves it to the repository.
        /// </summary>
        /// <param name="request">The command wrapper.</param>
        /// <param name="cancellationToken">Token to cancell the command.</param>
        /// <returns>The id of the created rental.</returns>
        public async Task<Guid> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            var rentedSpeakers = new List<Domain.Models.Speaker>();
            foreach(var m in request.SpeakerTrackerMappings)
            {
                var t = await _trackerRepo.GetAsync(m.TrackerHardwareId);
                var s = await _speakerRepo.GetAsync(m.SpeakerSerialNumber);
                s.AttachTracker(t);
                rentedSpeakers.Add(s);
            }
            var d = request.Destination;
            var location = await _locationService.AddressToLocation(d.Address, d.City, d.Province, d.PostalCode, "Canada");
            var destination = new Domain.Models.Venue(location, d.Address, d.City, d.Province, d.PostalCode);
            var c = request.Customer;
            var customer = new Domain.Models.Customer(c.Name, c.Phone, c.Email);
            var rental = new Domain.Models.Rental(rentedSpeakers, customer, request.RentalDate, request.ExpectedReturnDate, request.ArrivalDate, destination);
            await _rentalRepo.AddAsync(rental);
            var trackersToMap = rental.RentedSpeakers.Select(s => new MapPlotPointDto(rental.Id, rental.Customer.Name, s.SerialNumber, s.Model, new TrackerDto(s.Tracker.HardwareId, s.Tracker.LastUpdate, new LocationDto(s.Tracker.Location.Longitude, s.Tracker.Location.Latitude))));
            await _notificationService.Notify(new RentalCreatedNotification(trackersToMap));
            return rental.Id;
        }
    }
}
