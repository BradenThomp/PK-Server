using Application.Features.Map.Dtos;
using Application.Features.Map.Queries;
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
    public class MapController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MapController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MapPlotPointDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetMapQuery());
            return result.ToList();
        }
    }
}
