using Application.Features.EmailNotifications.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailSubscriptionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmailSubscriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Subscribe(SubscribeCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
