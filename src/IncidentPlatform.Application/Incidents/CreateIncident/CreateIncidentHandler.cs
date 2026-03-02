using IncidentPlatform.Application.Auth;
using IncidentPlatform.Application.Incidents.Ports;
using IncidentPlatform.Domain.Incidents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Incidents.CreateIncident
{
    public sealed class CreateIncidentHandler
    {
        private readonly IIncidentRepository _repo;
        private readonly ICurrentUser _currentUser;
        public CreateIncidentHandler(IIncidentRepository repo, ICurrentUser currentUser)
        {
            _repo = repo;
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

            await _repo.AddAsync(incident, ct);

            return new CreateIncidentResult(incident.Id);
        }
    }
}
