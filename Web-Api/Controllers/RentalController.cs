using Application.Features.Rentals;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web_Api.Controllers
{
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
        [Route("[action]")]
        public async Task CreateRental([FromBody] CreateRentalCommand command)
        {
            await _mediator.Send(command);
        }
    }
}
