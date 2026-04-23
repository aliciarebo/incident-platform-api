using IncidentPlatform.Application.Auth;
using IncidentPlatform.Domain.Ports;
using IncidentPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Incidents.ChangeIncidentStatus
{
    public class ChangeIncidentStatusHandler
    {
        private readonly IIncidentRepository _repository;
        private readonly ICurrentUser _currentUser;

        public ChangeIncidentStatusHandler(
            IIncidentRepository repository,
            ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public async Task<ChangeIncidentStatusResult> HandleAsync(ChangeIncidentStatusCommand command)
        {
            var incident = await _repository.GetByIdAsync(command.IncidentId);

            if (incident is null)
                throw new KeyNotFoundException("Incident not found.");

            var isAdmin = _currentUser.Role == UserRole.Admin;
            var isAgent = _currentUser.Role == UserRole.Agent;

            if (!isAdmin && !isAgent)
                throw new UnauthorizedAccessException("Only Agent and Admin can change status.");

            if (isAgent)
            {
                // solo sus incidencias
                if (incident.AssignedToId != _currentUser.UserId)
                    throw new UnauthorizedAccessException("Agent can only change status of their assigned incidents.");
            }

            // Dominio valida transición
            incident.ChangeStatus(command.Status);

            await _repository.UpdateAsync(incident);

            return new ChangeIncidentStatusResult(
                incident.Id,
                incident.Title,
                incident.Description,
                incident.Priority.ToString(),
                incident.Status.ToString(),
                incident.TeamId,
                incident.ReporterId,
                incident.AssignedToId,
                incident.CreatedAt,
                incident.UpdatedAt
            );
        }
    }
}
