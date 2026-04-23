using IncidentPlatform.Domain.Incidents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Incidents.ChangeIncidentStatus
{
    public sealed record ChangeIncidentStatusCommand(
    Guid IncidentId,
    IncidentStatus Status
    );

}
