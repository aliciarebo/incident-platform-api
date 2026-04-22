using IncidentPlatform.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Incidents.GetIncidents
{
    public class GetIncidentsHandler
    {
        private readonly IIncidentRepository _repository;

        public GetIncidentsHandler(IIncidentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetIncidentsResult>> HandleAsync(GetIncidentsQuery query)
        {
            var incidents = await _repository.GetAllAsync();

            return incidents.Select(i => new GetIncidentsResult(
                i.Id,
                i.Title,
                i.Description,
                i.Priority.ToString(),
                i.Status.ToString(),
                i.TeamId,
                i.ReporterId,
                i.AssignedToId,
                i.CreatedAt,
                i.UpdatedAt
            ));
        }
    }
}
