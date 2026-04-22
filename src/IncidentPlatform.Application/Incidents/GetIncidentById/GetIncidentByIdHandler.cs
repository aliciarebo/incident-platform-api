using IncidentPlatform.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Incidents.GetIncidentById
{
    public class GetIncidentByIdHandler
    {
        private readonly IIncidentRepository _repository;

        public GetIncidentByIdHandler(IIncidentRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetIncidentByIdResult?> HandleAsync(GetIncidentByIdQuery query)
        {
            var incident = await _repository.GetByIdAsync(query.Id);

            if (incident is null)
                return null;

            return new GetIncidentByIdResult(
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
