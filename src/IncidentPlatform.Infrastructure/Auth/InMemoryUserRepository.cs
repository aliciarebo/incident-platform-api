using IncidentPlatform.Application.Auth;
using IncidentPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Infrastructure.Auth
{
    public class InMemoryUserRepository: IUserRepository
    {
        private readonly List<User> _users =
        [
            new User(
                Guid.Parse("11111111-1111-1111-1111-111111111111"),
                "Agent User",
                "agent@incidentplatform.dev",
                UserRole.Agent,
                Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            ),
            new User(
                Guid.Parse("22222222-2222-2222-2222-222222222222"),
                "Admin User",
                "admin@incidentplatform.dev",
                UserRole.Admin,
                Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            ),
            new User(
                Guid.Parse("33333333-3333-3333-3333-333333333333"),
                "Reporter User",
                "reporter@incidentplatform.dev",
                UserRole.Reporter,
                Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            )
        ];
        public Task<User?> GetByEmailAsync(string email)
        {
            var user = _users.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(user);
        }
    }
}
