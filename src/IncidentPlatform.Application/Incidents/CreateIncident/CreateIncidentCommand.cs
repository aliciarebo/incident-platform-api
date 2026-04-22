using IncidentPlatform.Domain.Incidents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Incidents.CreateIncident
{
    public sealed record CreateIncidentCommand(
    string Title,
    string Description,
    IncidentPriority Priority,
    Guid TeamId,
    string? Category
    );
}
