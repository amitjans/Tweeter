using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Configuration;
using Tweeter.Models;
using Tweeter.Models.DTOs.Requests;

namespace Tweeter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly tweeterContext _context;
        private readonly JwtConfig _jwtConfig;

        public AccountController(tweeterContext context, IOptionsMonitor<Configuration.JwtConfig> optionsMonitor)
        {
            _context = context;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [Route("login")]
        [HttpGet]
        public IActionResult SignIn(UserLoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var hash = Sha256.GetHash(login.Password);
            User _user = _context.Users.Where(x => x.Username == login.User && x.Password == hash).FirstOrDefault();
            if (_user == null)
            {
                return NotFound();
            }

            return Ok(GenerateJwtToken(_user));
        }

        [HttpPost]
        //public async Task<ActionResult> SignUp(User login)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    User _user = _context.Users.Where(x => x.Username == login.user).FirstOrDefault();
        //    if (_user == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok("success");
        //}

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var claims = new[]
            {
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.GivenName, user.Lastname),
            new Claim(ClaimTypes.NameIdentifier, user.Username)
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                // Nuestro token va a durar un día
                Expires = DateTime.UtcNow.AddDays(1),
                // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);
        }
    }
}
