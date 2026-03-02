using IncidentPlatform.Domain.Incidents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Incidents.Ports
{
    public interface IIncidentRepository
    {
        Task AddAsync(Incident incident, CancellationToken ct = default);
    }
}
