using IncidentPlatform.Application.Incidents.CreateIncident;
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

        public IncidentsController(CreateIncidentHandler createHandler, GetIncidentsHandler getHandler)
        {
            _createHandler = createHandler;
            _getHandler = getHandler;
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
    }
}
