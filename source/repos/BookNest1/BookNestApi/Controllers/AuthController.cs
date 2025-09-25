using Microsoft.AspNetCore.Mvc;
using BookNestDAL;
using BookNestDAL.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace BookNestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthRepo _authRepo;
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _authRepo = new AuthRepo();
            _config = config;
        }

        // POST: /api/auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            var existingUser = _authRepo.GetUserByEmail(user.Email);
            if (existingUser != null)
                return BadRequest("User already exists");

            // You can hash password if you want
            // user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

            _authRepo.Register(user);
            return Ok("User registered successfully");
        }

        // POST: /api/auth/login
        
        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginUser)
        {
            var user = _authRepo.Login(loginUser.Email, loginUser.PasswordHash);
            if (user == null) return Unauthorized("Invalid credentials");

            // JWT generation directly here
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role)
    };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = jwtToken, role = user.Role, userId = user.UserId });
        }
    }
    }
