using Application.Features.Tracking;
using Application.Features.Tracking.Commands;
using Application.Features.Tracking.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost]
        public async Task<ActionResult> Register(RegisterTrackerCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("[Action]/{macAddress}")]
        public async Task<ActionResult> Location(string macAddress, UpdateTrackerLocationCommand command)
        {
            if(command.MACAddress != macAddress)
            {
                return BadRequest();
            }
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("[Action]/{id}")]
        public async Task<ActionResult> Assign(string id, AssignToSpeakerCommand command)
        {
            if (command.TrackerId != id)
            {
                return BadRequest();
            }
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet("{macAddress}")]
        public async Task<ActionResult<TrackerDto>> Get(string macAddress)
        {
            return await _mediator.Send(new GetTrackerQuery(macAddress));
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TrackerDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllTrackersQuery());
            return result.ToList();
        }
    }
}
