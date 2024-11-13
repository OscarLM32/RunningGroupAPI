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

	[HttpGet("clubs/{clubId}/users/{userId}")]
	public async Task<IActionResult> GetMembershipAsync(string clubId, string userId)
	{
		var membership = await _service.GetMembershipAsync(clubId, userId);
		if (membership == null) return NotFound("Membership not found for the specified user and club.");

		return Ok(membership);
	}
	#endregion

	#region CRUD
	[HttpPost]
	public async Task<IActionResult> AddUserToClub(AddUserToClubDTO addUserToClubDto)
	{
		if(!ModelState.IsValid) return BadRequest();
		
		var success = await _service.AddUserToClub(addUserToClubDto);
		if(!success) return StatusCode(500, "An error occurred while adding the user to the club.");

		return CreatedAtAction(nameof(GetMembershipAsync), new { clubId = addUserToClubDto.ClubId, userId = addUserToClubDto.AppUserId }, addUserToClubDto);
	}
	#endregion
	
	[HttpGet("clubs/{clubId}/is-owner/{userId}")]
	public async Task<IActionResult> IsOwner(string clubId, string userId)
	{
		var isOwner = await _service.IsOwner(clubId, userId);
		return Ok( new{isOwner} );
	}
}