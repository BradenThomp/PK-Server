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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
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
