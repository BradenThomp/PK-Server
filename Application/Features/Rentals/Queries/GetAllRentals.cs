using Application.Common.Repository;
using Application.Features.Rentals.Dtos;
using Application.Features.Speaker.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Queries
{
    public record GetAllRentalsQuery() : IRequest<IEnumerable<RentalDto>>;

    public class GetAllRentalsQueryHandler : IRequestHandler<GetAllRentalsQuery, IEnumerable<RentalDto>>
    {
        private readonly IRentalRepository _repo;

        public GetAllRentalsQueryHandler(IRentalRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RentalDto>> Handle(GetAllRentalsQuery request, CancellationToken cancellationToken)
        {
            var rentals = await _repo.GetAllAsync();
            return rentals.Select(r =>
            {
                var c = new CustomerDto(r.Customer.Name, r.Customer.Phone, r.Customer.Email);
                var d = new VenueDto(r.Destination.Address, r.Destination.City, r.Destination.Province, r.Destination.PostalCode);
                var s = r.RentedSpeakers.Select(s => new SpeakerDto(s.SerialNumber, s.Model, new Tracking.Dtos.TrackerDto(s.Tracker.HardwareId, s.Tracker.LastUpdate, new Tracking.Dtos.LocationDto(s.Tracker.Location.Longitude, s.Tracker.Location.Latitude))));
                return new RentalDto(r.Id, r.RentalDate, r.ExpectedReturnDate, c, d, s);
            });
        }
    }
}
