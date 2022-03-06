using Application.Features.Tracking.Commands;
using Application.Features.Tracking.Dtos;
using Application.Features.Tracking.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TrackerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TrackerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Create(CreateTrackerCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("{hardwareId}")]
        public async Task<ActionResult> Update(string hardwareId, UpdateTrackerCommand command)
        {
            if(command.HardwareId != hardwareId)
            {
                return BadRequest();
            }
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet("{hardwareId}")]
        public async Task<ActionResult<TrackerDto>> Get(string hardwareId)
        {
            return await _mediator.Send(new GetTrackerQuery(hardwareId));
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TrackerDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllTrackersQuery());
            return result.ToList();
        }
    }
}
