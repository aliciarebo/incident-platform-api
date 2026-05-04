using IncidentPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Domain.Ports
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);

    }
}
