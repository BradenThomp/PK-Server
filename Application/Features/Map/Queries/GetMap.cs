using Application.Common.Repository;
using Application.Features.Map.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Tracking.Dtos;
using AutoMapper;
using Domain.Models;

namespace Application.Features.Map.Queries
{
    public record GetMapQuery() : IRequest<IEnumerable<MapPlotPointDto>>;

    public class GetAllRentalsQueryHandler : IRequestHandler<GetMapQuery, IEnumerable<MapPlotPointDto>>
    {
        private readonly IRentalRepository _repo;
        private readonly IMapper _mapper;

        public GetAllRentalsQueryHandler(IRentalRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MapPlotPointDto>> Handle(GetMapQuery request, CancellationToken cancellationToken)
        {
            var rentals = await _repo.GetAllAsync();
            var openRentals = rentals.Where(r => !r.IsReturned);
            List<MapPlotPointDto> result = new List<MapPlotPointDto>();
            foreach (var rental in openRentals)
            {
                var plotPoints = rental.RentedSpeakers.Select(s => new MapPlotPointDto(rental.Id, rental.Customer.Name, s.SerialNumber, s.Model, _mapper.Map<Tracker, TrackerDto>(s.Tracker)));
                result.AddRange(plotPoints);
            }
            return result;
        }
    }
}
