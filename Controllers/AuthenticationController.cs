using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunningGroupAPI.Data;
using RunningGroupAPI.DTOs;
using RunningGroupAPI.Interfaces;
using RunningGroupAPI.Models;

namespace RunningGroupAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
	private readonly AppDbContext _dbContext;
	private readonly UserManager<AppUser> _userManager;
	private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthenticationController(AppDbContext dbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
	{
		_dbContext = dbContext;
		_userManager = userManager;
		_signInManager = signInManager;
		_tokenService = tokenService; 
	}
	
	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterDTO data)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var user = new AppUser { Email = data.Email, UserName = data.Email };
		var result = await _userManager.CreateAsync(user, data.Password);

		if (result.Succeeded)
		{
			// Optionally assign user to a default role
			await _userManager.AddToRoleAsync(user, UserRoles.User);

			return Ok(new { Message = "User registered successfully" });
		}

		return BadRequest(result.Errors);
	}
	
	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginDTO data)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var user = await _userManager.FindByEmailAsync(data.Email);
		if (user == null)
		{
			return Unauthorized(new { Message = "Invalid credentials" }); // 401 Unauthorized
		}
			
		bool passwordCheckPassed = !await _userManager.CheckPasswordAsync(user, data.Password);
		if (!passwordCheckPassed)
		{
			return Unauthorized(new { Message = "Invalid credentials" }); // 401 Unauthorized
		}

		var token = _tokenService.GenerateToken(user);

		return Ok(new { Token = token }); // 200 OK with token
	}
}