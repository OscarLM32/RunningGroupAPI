using Microsoft.AspNetCore.Mvc;
using RunningGroupAPI.DTOs.Club;
using RunningGroupAPI.Interfaces.Services;

namespace RunningGroupAPI.Controllers;

[ApiController]
[Route("api/clubs")]
public class ClubController : ControllerBase
{
	private readonly IClubService _clubService;

	public ClubController(IClubService clubService)
	{
		_clubService = clubService;
	}
	
	[HttpGet]
	public async Task<IActionResult> GetAllClubs()
	{
		var clubs = await _clubService.GetAllClubsAsync();
		return Ok(clubs);
	}
	
	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetClubById(int id)
	{
		var club = await _clubService.GetClubByIdAsync(id);
		if(club == null)
		{
			return NotFound();	
		}
		
		return Ok(club);
	}
	
	[HttpGet("city/{city}")]
	public async Task<ActionResult<IEnumerable<ClubDTO>>> GetClubsByCity(string city)
	{
		var clubs = await _clubService.GetClubsByCityAsync(city);
		return Ok(clubs);
	}
	
	[HttpPost]
	public async Task<IActionResult> CreateClub([FromBody] CreateClubDTO createClubDto)
	{
		if(!ModelState.IsValid) return BadRequest(ModelState);
		
		int clubId = _clubService.AddClub(createClubDto);
		if(clubId < 0) return StatusCode(500, "An error occurred while creating the club.");

		return CreatedAtAction(nameof(GetClubById), new { id = clubId }, createClubDto);
	}

	[HttpPut("{id:int}")]
	public async Task<ActionResult> UpdateClub(int id, [FromForm] UpdateClubDTO updateClubDto)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState);

		bool result = _clubService.UpdateClub(id, updateClubDto);

		if (!result) return StatusCode(500, "An error occurred while updating the club.");

		return Ok(new { Message = "Club updated successfully." });
	}
	
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> RemoveClub(int id)
	{
		bool result = _clubService.RemoveClub(id);
		if(!result) return StatusCode(500, "An error occurred while removing the club.");

		return Ok(new { Message = "Club removed successfully." });		
	}
}