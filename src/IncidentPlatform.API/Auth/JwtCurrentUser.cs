using IncidentPlatform.Application.Auth;
using IncidentPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace IncidentPlatform.API.Auth
{
    public class JwtCurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtCurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal User =>
            _httpContextAccessor.HttpContext?.User
            ?? throw new InvalidOperationException("No HttpContext available");

        public Guid UserId =>
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new InvalidOperationException("User id claim not found"));

        public UserRole Role =>
            Enum.Parse<UserRole>(
                User.FindFirst(ClaimTypes.Role)?.Value
                    ?? throw new InvalidOperationException("Role claim not found"));

        public Guid TeamId =>
            Guid.Parse(User.FindFirst("teamId")?.Value
                ?? throw new InvalidOperationException("Team id claim not found"));
    }
}
