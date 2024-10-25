using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunningGroupAPI.DTOs;

namespace RunningGroupAPI.Interfaces;

public interface IAuthenticationService
{
	public Task<IdentityResult> RegisterUserAsync(RegisterDTO register);
	public Task<string> LoginUserAsync(LoginDTO login);
}