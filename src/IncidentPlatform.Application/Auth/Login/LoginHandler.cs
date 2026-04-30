using IncidentPlatform.Domain.Users;
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

        public LoginHandler(
        IUserRepository userRepository,
        ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginResult> HandleAsync(LoginCommand command)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);

            if (user is null)
                throw new UnauthorizedAccessException("Invalid credentials");

            // MVP: sin password real todavía
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
