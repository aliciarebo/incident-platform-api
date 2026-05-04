using IncidentPlatform.Application.Auth;
using IncidentPlatform.Domain.Ports;
using IncidentPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Incidents.GetTeamIncidents
{
    public class GetTeamIncidentsHandler
    {
        private readonly IIncidentRepository _repository;

        public GetTeamIncidentsHandler(
            IIncidentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetTeamIncidentsResult>> HandleAsync(GetTeamIncidentsQuery query)
        {            

            var incidents = await _repository.GetAllAsync();

            var result = incidents.Where(i => i.TeamId == query.TeamId);

            if (!string.IsNullOrWhiteSpace(query.Status))
            {
                result = result.Where(i =>
                    i.Status.ToString().Equals(query.Status, StringComparison.OrdinalIgnoreCase));
            }

            if (query.Assigned.HasValue)
            {
                if (query.Assigned.Value)
                    result = result.Where(i => i.AssignedToId != null);
                else
                    result = result.Where(i => i.AssignedToId == null);
            }

            return result.Select(i => new GetTeamIncidentsResult(
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
