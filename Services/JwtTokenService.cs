using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RunningGroupAPI.Interfaces;
using RunningGroupAPI.Models;

namespace RunningGroupAPI.Services;

public class JwtTokenService : ITokenService
{
	private readonly IConfiguration _configuration;

	// Inject IConfiguration through the constructor
	public JwtTokenService(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public string GenerateToken(AppUser user)
	{
		Claim[] claims =
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken
		(
			issuer: _configuration["Jwt:Issuer"],
			audience: _configuration["Jwt:Audience"],
			claims: claims,
			expires: DateTime.Now.AddMinutes(30),
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}