using Microsoft.AspNetCore.Mvc;
using RunningGroupAPI.DTOs.ClubMembership;
using RunningGroupAPI.Interfaces.Services;

namespace RunningGroupAPI.Controllers;

[ApiController]
[Route("api/clubMemberships")]
public class ClubMembershipController : Controller
{
	private readonly IClubMembershipService _service;

	public ClubMembershipController(IClubMembershipService service)
	{
		_service = service;
	}
	
	#region GETTERS
	[HttpGet]
	public async Task<IActionResult> GetAllMemeberships()
	{
		var memberships = await _service.GetAllMembershipsAsync();
		return Ok(memberships);
	}

	[HttpGet("users/{id}")]
	public async Task<IActionResult> GetUserMemberships(string id)
	{
		var memberships = await _service.GetUserMembershipsAsync(id);
		return Ok(memberships);
	}

	[HttpGet("clubs/{id}")]
	public async Task<IActionResult> GetClubsMemberships(string id)
	{
		var memberships = await _service.GetClubMembershipsAsync(id);
		return Ok(memberships);
	}
	[HttpGet("clubs/{clubId}/is-owner/{userId}")]
	public Task<bool> IsOwner(string clubId, string userId)
	{
		return _service.IsOwner(clubId, userId);
	}
}