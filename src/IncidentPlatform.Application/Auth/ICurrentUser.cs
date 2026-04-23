using IncidentPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Auth
{
    public interface ICurrentUser
    {
        Guid UserId { get; }
        Guid TeamId { get; }
        UserRole Role { get; }
    }
}
