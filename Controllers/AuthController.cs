using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketSystemAPI.Models;

namespace TicketSystemAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string SECRET_KEY = "MinhaChaveSecretaSuperSegura";
        private readonly SymmetricSecurityKey _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            if (login.Username != "admin" || login.Password != "1234")
                return Unauthorized("Usuário ou senha inválidos!");

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, login.Username) }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { Token = tokenHandler.WriteToken(token) });
        }
    }
}