using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        
        /**([HttpPost]
        public async Task<ActionResult> Create(CreateRentalCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }**/
    }
}
