using IncidentPlatform.Domain.Incidents;

namespace IncidentPlatform.API.Contracts.Incidents
{
    public sealed record ChangeIncidentStatusRequest(
    IncidentStatus Status
    );
}
