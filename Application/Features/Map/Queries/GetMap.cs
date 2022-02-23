﻿using Application.Common.Repository;
using Application.Features.Map.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Tracking.Dtos;

namespace Application.Features.Map.Queries
{
    public record GetMapQuery() : IRequest<IEnumerable<MapPlotPointDto>>;

    public class GetAllRentalsQueryHandler : IRequestHandler<GetMapQuery, IEnumerable<MapPlotPointDto>>
    {
        private readonly IRentalRepository _repo;

        public GetAllRentalsQueryHandler(IRentalRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<MapPlotPointDto>> Handle(GetMapQuery request, CancellationToken cancellationToken)
        {
            var rentals = await _repo.GetAllAsync();
            var openRentals = rentals.Where(r => !r.IsReturned);
            List<MapPlotPointDto> result = new List<MapPlotPointDto>();
            foreach (var rental in openRentals)
            {
                var plotPoints = rental.RentedSpeakers.Select(s => new MapPlotPointDto(rental.Id, rental.Customer.Name, s.SerialNumber, s.Model, new TrackerDto(s.Tracker.HardwareId, s.Tracker.LastUpdate, new LocationDto(s.Tracker.Location.Longitude, s.Tracker.Location.Latitude))));
                result.AddRange(plotPoints);
            }
            return result;
        }
    }
}