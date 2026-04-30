using IncidentPlatform.API.Auth;
using IncidentPlatform.Application.Auth.Login;
using Microsoft.AspNetCore.Mvc;

namespace IncidentPlatform.API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly LoginHandler _loginHandler;

        public AuthController(LoginHandler loginHandler)
        {
            _loginHandler = loginHandler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var command = new LoginCommand(
                request.Email,
                request.Password
            );

            var result = await _loginHandler.HandleAsync(command);

            return Ok(new
            {
                accessToken = result.AccessToken,
                user = new
                {
                    id = result.Id,
                    name = result.Name,
                    role = result.Role.ToString(),
                    teamId = result.TeamId
                }
            });
        }
    }
}
