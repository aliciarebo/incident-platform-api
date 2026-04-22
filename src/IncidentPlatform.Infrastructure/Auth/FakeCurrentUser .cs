using IncidentPlatform.Application.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Infrastructure.Auth
{
    public class FakeCurrentUser : ICurrentUser
    {
        public Guid UserId => Guid.Parse("11111111-1111-1111-1111-111111111111");
        public string Role => "Agent";
        public Guid TeamId => Guid.Parse("11111111-1111-1111-1111-111111111111");
    }
}
