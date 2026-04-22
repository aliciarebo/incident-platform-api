
using IncidentPlatform.Domain.Incidents;
using IncidentPlatform.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Infrastructure.Persistence
{
    public class InMemoryIncidentRepository : IIncidentRepository
    {
        private readonly List<Incident> _incidents = new();
        public Task AddAsync(Incident incident, CancellationToken ct = default)
        {
            _incidents.Add(incident);
            return Task.CompletedTask;
        }

        public Task<Incident?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var incident = _incidents.FirstOrDefault(i => i.Id == id);
            return Task.FromResult(incident);
        }

        public Task<IEnumerable<Incident>> GetAllAsync()
        {
            return Task.FromResult(_incidents.AsEnumerable());
        }
    }
}
