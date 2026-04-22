using IncidentPlatform.Application.Incidents.CreateIncident;
using IncidentPlatform.Application.Incidents.GetIncidentById;
using IncidentPlatform.Application.Incidents.GetIncidents;
using Microsoft.AspNetCore.Mvc;

namespace IncidentPlatform.API.Controllers
{
    [ApiController]
    [Route("api/v1/incidents")]
    public class IncidentsController: ControllerBase
    {
        private readonly CreateIncidentHandler _createHandler;
        private readonly GetIncidentsHandler _getHandler;
        private readonly GetIncidentByIdHandler _getByIdHandler;

        public IncidentsController(CreateIncidentHandler createHandler, GetIncidentsHandler getHandler, GetIncidentByIdHandler getHandlerById)
        {
            _createHandler = createHandler;
            _getHandler = getHandler;
            _getByIdHandler = getHandlerById;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIncidentCommand request)
        {
            var result = await _createHandler.HandleAsync(request);

            return CreatedAtAction(
                nameof(Create),
                new { id = result.Id },
                result
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getHandler.HandleAsync(new GetIncidentsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _getByIdHandler.HandleAsync(new GetIncidentByIdQuery(id));

            if (result is null)
                return NotFound();

            return Ok(result);
        }
    }
}
