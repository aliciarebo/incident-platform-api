using IncidentPlatform.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Application.Auth.Login
{
    public class LoginHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;

        public LoginHandler(
        IUserRepository userRepository,
        ITokenService tokenService,
        IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginResult> HandleAsync(LoginCommand command)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);

            if (user is null)
                throw new UnauthorizedAccessException("Invalid credentials");

            if (user is null || !_passwordHasher.Verify(command.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = _tokenService.GenerateToken(user);

            return new LoginResult(
                token,
                user.Id,
                user.Name,
                user.Role,
                user.TeamId
            );
        }
    }
}
