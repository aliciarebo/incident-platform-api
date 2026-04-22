using IncidentPlatform.Application.Incidents.CreateIncident;
using Microsoft.AspNetCore.Mvc;

namespace IncidentPlatform.API.Controllers
{
    [ApiController]
    [Route("api/v1/incidents")]
    public class IncidentsController: ControllerBase
    {
        private readonly CreateIncidentHandler _handler;

        public IncidentsController(CreateIncidentHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIncidentCommand request)
        {
            var result = await _handler.HandleAsync(request);

            return CreatedAtAction(
                nameof(Create),
                new { id = result.Id },
                result
            );
        }
    }
}
