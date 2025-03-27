using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly SymmetricSecurityKey _key;

    public AuthController()
    {
        var SECRET_KEY = "MinhaChaveSecretaSuperSegura"; // Pode vir de appsettings.json
        _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest login)
    {
        if (login == null || string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            return BadRequest("Usuário e senha são obrigatórios.");

        if (login.Username != "admin" || login.Password != "1234")
            return Unauthorized("Usuário ou senha inválidos!");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, login.Username)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature)
        };


        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Ok(new { Token = tokenHandler.WriteToken(token) });
    }
}

public class LoginRequest
{

    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string Password { get; set; }
    [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
    public string Username { get; set; }

}