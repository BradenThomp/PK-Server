using Application.Common.Repository;
using Application.Features.Rentals.Dtos;
using Application.Features.Speaker.Dtos;
using AutoMapper;
using Domain.Models;
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
        private readonly IMapper _mapper;

        public GetAllRentalsQueryHandler(IRentalRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RentalDto>> Handle(GetAllRentalsQuery request, CancellationToken cancellationToken)
        {
            var rentals = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<Rental>, IEnumerable<RentalDto>>(rentals);
        }
    }
}
