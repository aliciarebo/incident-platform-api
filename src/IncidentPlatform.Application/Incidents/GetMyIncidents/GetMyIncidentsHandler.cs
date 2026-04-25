using IncidentPlatform.Application.Auth;
using IncidentPlatform.Domain.Ports;
using IncidentPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Incidents.GetMyIncidents
{
    public class GetMyIncidentsHandler
    {
        private readonly IIncidentRepository _repository;
        private readonly ICurrentUser _currentUser;

        public GetMyIncidentsHandler(
            IIncidentRepository repository,
            ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public async Task<IEnumerable<GetMyIncidentsResult>> HandleAsync(GetMyIncidentsQuery query)
        {

            var incidents = await _repository.GetAllAsync();

            var result = incidents
                .Where(i => i.AssignedToId == _currentUser.UserId);

            return result.Select(i => new GetMyIncidentsResult(
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
