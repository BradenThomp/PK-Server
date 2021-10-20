using Application.Features.Tracking;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TrackerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task UpdateTrackerLocation([FromBody] UpdateTrackerLocationCommand request)
        {
            await _mediator.Send(request);
        }
    }
}
