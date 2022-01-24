using Application.Features.Rentals;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> Create(CreateRentalCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
