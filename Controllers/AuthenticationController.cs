using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunningGroupAPI.DTOs.Authentication;
using RunningGroupAPI.Interfaces.Services;

namespace RunningGroupAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
	private readonly IAuthenticationService _authService;

	public AuthenticationController(IAuthenticationService authService)
	{
		_authService = authService;
	}
	
	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterDTO data)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState);

		var result = await _authService.RegisterUserAsync(data);
		
		if(result.Succeeded)
		{
			return Ok(new { Message = "User registered successfully"});
		}

		return BadRequest(result.Errors); 
	}
	
	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginDTO data)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState);

		var token = await _authService.LoginUserAsync(data);
		
		if(token == null)
		{
			return Unauthorized(new { Message = "Invalid credentials" }); 
		}

		return Ok(new { Token = token }); 
	}
	
	
	[HttpPost("logout")]
	[Authorize]
	public async Task<IActionResult> Logout()
	{
		return Ok(new { Message = "User logged out successfully" });
	}
}