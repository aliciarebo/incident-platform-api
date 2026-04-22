using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Incidents.GetIncidentById
{
    public sealed record GetIncidentByIdResult(
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
