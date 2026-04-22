using IncidentPlatform.Application.Auth;
using IncidentPlatform.Domain.Incidents;
using IncidentPlatform.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Incidents.CreateIncident
{
    public sealed class CreateIncidentHandler
    {
        private readonly IIncidentRepository _incidentRepo;
        private readonly ICurrentUser _currentUser;
        public CreateIncidentHandler(IIncidentRepository repo, ICurrentUser currentUser)
        {
            _incidentRepo = repo;
            _currentUser = currentUser;
        }

        public async Task<CreateIncidentResult> HandleAsync(CreateIncidentCommand cmd, CancellationToken ct = default)
        {
            var incident = new Incident(
                id: Guid.NewGuid(),
                title: cmd.Title,
                description: cmd.Description,
                priority: cmd.Priority,
                teamId: cmd.TeamId,
                category: cmd.Category,
                reporterId: _currentUser.UserId
            );

            await _incidentRepo.AddAsync(incident, ct);

            return new CreateIncidentResult(incident.Id);
        }
    }
}
