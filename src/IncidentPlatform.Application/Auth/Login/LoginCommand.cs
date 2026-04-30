using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Auth.Login
{
    public sealed record LoginCommand(
    string Email,
    string Password
    );
}
