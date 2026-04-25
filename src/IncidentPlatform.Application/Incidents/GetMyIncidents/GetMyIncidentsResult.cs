using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Incidents.GetMyIncidents
{
    public sealed record GetMyIncidentsResult(
    Guid Id,
    string Title,
    string Description,
    string Priority,
    string Status,
    Guid TeamId,
    Guid ReporterId,
    Guid? AssignedToId,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt
);
}
