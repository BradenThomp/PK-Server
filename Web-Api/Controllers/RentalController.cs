using Application.Features.Rentals.Commands;
using Application.Features.Rentals.Dtos;
using Application.Features.Rentals.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RentalController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateRentalCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<RentalDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllRentalsQuery());
            return result.ToList();
        }
    }
}
