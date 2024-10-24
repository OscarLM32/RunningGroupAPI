using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunningGroupAPI.Data;
using RunningGroupAPI.Models;

namespace RunningGroupAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
	private readonly AppDbContext _dbContext;
	private readonly UserManager<AppUser> _userManager;
	private readonly SignInManager<AppUser> _signInManager;

	public AuthenticationController(AppDbContext dbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
	{
		_dbContext = dbContext;
		_userManager = userManager;
		_signInManager = signInManager;
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
}