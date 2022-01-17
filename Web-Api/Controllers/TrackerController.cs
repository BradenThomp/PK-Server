using Application.Features.Tracking;
using Application.Features.Tracking.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        [Route("api/[controller]/[action]")]
        public async Task RegisterTracker([FromBody] RegisterTrackerCommand command)
        {
            await _mediator.Send(command);
        }

        [HttpPut]
        [Route("api/[controller]/[action]")]
        public async Task UpdateTrackerLocation([FromBody] UpdateTrackerLocationCommand command)
        {
            await _mediator.Send(command);
        }

        [HttpGet]
        [Route("api/[controller]/[action]/{macAddress}")]
        public async Task<ActionResult<TrackerDto>> GetTracker(string macAddress)
        {
            return await _mediator.Send(new GetTrackerQuery(macAddress));
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<IEnumerable<TrackerDto>> GetAll()
        {
            return await _mediator.Send(new GetAllTrackersQuery());
        }
    }
}
