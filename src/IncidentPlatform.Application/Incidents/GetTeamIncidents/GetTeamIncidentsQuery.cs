using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Incidents.GetTeamIncidents
{
    public sealed record GetTeamIncidentsQuery(
        Guid TeamId,
        string? Status,
        bool? Assigned
    );
}
