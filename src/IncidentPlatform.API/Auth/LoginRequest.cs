namespace IncidentPlatform.API.Auth
{
    public sealed record LoginRequest(
    string Email,
    string Password
    );
}
