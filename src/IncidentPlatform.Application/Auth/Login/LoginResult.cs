using IncidentPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Auth.Login
{
    public sealed record LoginResult(
    string AccessToken,
    Guid Id,
    string Name,
    UserRole Role,
    Guid TeamId
    );
}
