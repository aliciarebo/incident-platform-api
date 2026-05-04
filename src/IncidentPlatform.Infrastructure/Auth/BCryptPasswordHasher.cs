using IncidentPlatform.Application.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Infrastructure.Auth
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        public bool Verify(string password, string passwordHash)
            => BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
