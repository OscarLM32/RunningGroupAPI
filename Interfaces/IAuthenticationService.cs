using Microsoft.AspNetCore.Identity;
using RunningGroupAPI.DTOs.Authentication;

namespace RunningGroupAPI.Interfaces;

public interface IAuthenticationService
{
	public Task<IdentityResult> RegisterUserAsync(RegisterDTO register);
	public Task<string> LoginUserAsync(LoginDTO login);
}