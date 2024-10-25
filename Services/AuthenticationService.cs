
using Microsoft.AspNetCore.Identity;
using RunningGroupAPI.Data;
using RunningGroupAPI.DTOs.Authentication;
using RunningGroupAPI.Interfaces.Services;
using RunningGroupAPI.Models;

namespace RunningGroupAPI.Services;

public class AuthentiCationService : IAuthenticationService
{
	
	private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public AuthentiCationService(UserManager<AppUser> userManager, ITokenService tokenService)
	{
		_userManager = userManager;
		_tokenService = tokenService;
	}

	public async Task<IdentityResult> RegisterUserAsync(RegisterDTO register)
	{
		var user = new AppUser { Email = register.Email, UserName = register.Email };
		var result = await _userManager.CreateAsync(user, register.Password);

		if (result.Succeeded)
		{
			await _userManager.AddToRoleAsync(user, UserRoles.User);
		}
		
		return result;
	}

	public async Task<string> LoginUserAsync(LoginDTO login)
	{
		var user = await _userManager.FindByEmailAsync(login.Email);
		if (user == null)
		{
			return null;
		}

		bool passwordCheckPassed = await _userManager.CheckPasswordAsync(user, login.Password);
		if (!passwordCheckPassed)
		{
			return null;
		}

		return _tokenService.GenerateToken(user);
	}
}