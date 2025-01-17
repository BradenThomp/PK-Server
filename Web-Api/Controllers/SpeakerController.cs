﻿using Application.Features.Speaker.Commands;
using Application.Features.Speaker.Dtos;
using Application.Features.Speaker.Queries;
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
    public class SpeakerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SpeakerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddSpeakerCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{serialNumber}")]
        public async Task<ActionResult<SpeakerDto>> Get(string serialNumber)
        {
            return await _mediator.Send(new GetSpeakerQuery(serialNumber));
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<SpeakerDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllSpeakersQuery());
            return result.ToList();
        }
    }
}
