using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Incidents.AssignIncident
{
    public sealed record AssignIncidentCommand(Guid IncidentId, Guid AssignedToId);
}
