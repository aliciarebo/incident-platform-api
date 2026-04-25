using IncidentPlatform.API.Contracts.Incidents;
using IncidentPlatform.Application.Incidents.AssignIncident;
using IncidentPlatform.Application.Incidents.ChangeIncidentStatus;
using IncidentPlatform.Application.Incidents.CreateIncident;
using IncidentPlatform.Application.Incidents.GetIncidentById;
using IncidentPlatform.Application.Incidents.GetIncidents;
using IncidentPlatform.Application.Incidents.GetMyIncidents;
using IncidentPlatform.Application.Incidents.GetTeamIncidents;
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
        private readonly AssignIncidentHandler _assignIncidentHandler;
        private readonly ChangeIncidentStatusHandler _changeIncidentStatusHandler;
        private readonly GetTeamIncidentsHandler _getTeamIncidentsHandler;
        private readonly GetMyIncidentsHandler _getMyIncidentsHandler;

        public IncidentsController(CreateIncidentHandler createHandler, GetIncidentsHandler getHandler, GetIncidentByIdHandler getHandlerById, AssignIncidentHandler assignIncidentHandler, ChangeIncidentStatusHandler changeIncidentStatusHandler, GetTeamIncidentsHandler getTeamIncidentsHandler, GetMyIncidentsHandler getMyIncidentsHandler )
        {
            _createHandler = createHandler;
            _getHandler = getHandler;
            _getByIdHandler = getHandlerById;
            _assignIncidentHandler = assignIncidentHandler;
            _changeIncidentStatusHandler = changeIncidentStatusHandler;
            _getTeamIncidentsHandler = getTeamIncidentsHandler;
            _getMyIncidentsHandler = getMyIncidentsHandler;
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

        [HttpPatch("{id}/assign")]
        public async Task<IActionResult> Assign(Guid id, AssignIncidentRequest request)
        {
            var command = new AssignIncidentCommand(id, request.AssignedToId);
            var result = await _assignIncidentHandler.HandleAsync(command);
            return Ok(result);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus(Guid id, ChangeIncidentStatusRequest request)
        {
            var command = new ChangeIncidentStatusCommand(id, request.Status);

            var result = await _changeIncidentStatusHandler.HandleAsync(command);

            return Ok(result);
        }

        [HttpGet("team")]
        public async Task<IActionResult> GetTeam([FromQuery] Guid teamId, [FromQuery] string? status, [FromQuery] bool? assigned)
        {
            var query = new GetTeamIncidentsQuery(teamId,status, assigned);

            var result = await _getTeamIncidentsHandler.HandleAsync(query);

            return Ok(result);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMy()
        {
            var result = await _getMyIncidentsHandler.HandleAsync(new GetMyIncidentsQuery());

            return Ok(result);
        }
    }
}
