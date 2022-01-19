using Application.Features.Speaker.Commands;
using Application.Features.Speaker.Dtos;
using Application.Features.Speaker.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SpeakerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        [Route("[action]")]
        public async Task AddSpeaker([FromBody] AddSpeakerCommand command)
        {
            await _mediator.Send(command);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<SpeakerDto>> GetAllSpeakers()
        {
            return await _mediator.Send(new GetAllSpeakersQuery());
        }
    }
}
