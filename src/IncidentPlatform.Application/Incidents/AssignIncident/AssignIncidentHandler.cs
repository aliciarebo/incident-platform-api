using IncidentPlatform.Application.Auth;
using IncidentPlatform.Domain.Ports;
using IncidentPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Incidents.AssignIncident
{
    public class AssignIncidentHandler
    {
        private readonly IIncidentRepository _repository;
        private readonly ICurrentUser _currentUser;

        public AssignIncidentHandler(IIncidentRepository repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public async Task<AssignIncidentResult> HandleAsync(AssignIncidentCommand command)
        {
            var incident = await _repository.GetByIdAsync(command.IncidentId);

            if (incident is null)
                throw new KeyNotFoundException("Incident not found.");

            var isAdmin = _currentUser.Role == UserRole.Admin;
            var isAgent = _currentUser.Role == UserRole.Agent;

            if (!isAdmin && !isAgent)
                throw new UnauthorizedAccessException("Only Agent and Admin can assign incidents.");

            if (isAgent)
            {
                if (command.AssignedToId != _currentUser.UserId)
                    throw new UnauthorizedAccessException("An agent can only assign incidents to themselves.");

                if (incident.TeamId != _currentUser.TeamId)
                    throw new UnauthorizedAccessException("An agent can only assign incidents from their own team.");
            }

            if (isAdmin)
            {
                // MVP: por ahora asumimos que el AssignedToId pertenece al equipo correcto.
                // Más adelante esto se valida contra usuarios/equipos reales.
            }

            incident.AssignTo(command.AssignedToId, isAdmin);

            await _repository.UpdateAsync(incident);

            return new AssignIncidentResult(
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
