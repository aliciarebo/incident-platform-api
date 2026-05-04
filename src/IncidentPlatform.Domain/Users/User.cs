using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Domain.Users
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public UserRole Role { get; private set; }
        public Guid TeamId { get; private set; }

        public User(Guid id, string name, string email, string passwordHash, UserRole role, Guid teamId)
        {
            if (id == Guid.Empty) throw new ArgumentException("Id is required", nameof(id));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required", nameof(name));
            if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentException("PasswordHash is required", nameof(passwordHash));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required", nameof(email));
            if (teamId == Guid.Empty) throw new ArgumentException("TeamId is required", nameof(teamId));

            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            TeamId = teamId;
        }
    }
}
