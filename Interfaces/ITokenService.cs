using RunningGroupAPI.Models;

namespace RunningGroupAPI.Interfaces;

public interface ITokenService
{
	public string GenerateToken(AppUser user);
}