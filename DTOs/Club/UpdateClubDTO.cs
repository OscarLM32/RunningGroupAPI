using RunningGroupAPI.Data.Enum;

namespace RunningGroupAPI.DTOs.Club;

public class UpdateClubDTO
{
	public string? Title { get; set; }
	public string? Description { get; set; }
	public IFormFile? Image { get; set; }

	public string? Country { get; set; }
	public string? City { get; set; }
	public string? Address { get; set; }

	public ClubCategory? ClubCategory { get; set; }
}