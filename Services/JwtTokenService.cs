using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RunningGroupAPI.Interfaces.Services;
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
			new Claim(JwtRegisteredClaimNames.Sub, user.Id),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT_SECRET_KEY"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken
		(
			issuer: _configuration["JWT_ISSUER"],
			audience: _configuration["JWT_AUDIENCE"],
			claims: claims,
			expires: DateTime.Now.AddMinutes(30),
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}