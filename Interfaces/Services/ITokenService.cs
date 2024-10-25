using RunningGroupAPI.Models;

namespace RunningGroupAPI.Interfaces.Services;

public interface ITokenService
{
	public string GenerateToken(AppUser user);
}