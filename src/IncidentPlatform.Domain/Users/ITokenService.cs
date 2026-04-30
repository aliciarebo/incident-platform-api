using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Domain.Users
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
