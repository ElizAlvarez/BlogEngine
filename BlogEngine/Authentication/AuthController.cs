using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogEngine.Core.Entities;
using BlogEngine.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BlogEngine.Authentication
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
		{
            _configuration = configuration;
            _userService = userService;
        }

		[HttpPost("login")]
		public ActionResult<User> Login(UserAuth user)
		{
			var result = _userService.GetUser(user);
			if (result == null)
			{
				BadRequest("User not found.");
			}

			var token = CreateToken(user);

			return Ok(token);
		}

        private string CreateToken(UserAuth user)
		{
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!);
			var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
			{
				new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()!),
				new(JwtRegisteredClaimNames.Sub, user.Email),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Name, user.UserName!),
                new("roleid", user.RoleId.ToString()!)
            };

			var token = new JwtSecurityToken("https://localhost:7004",
				"https://localhost:7004",
				claims,
				DateTime.UtcNow,
				DateTime.UtcNow.AddHours(2),
				signingCredentials
			);

			var jwt = tokenHandler.WriteToken(token);

			return jwt;
		}
    }
}

