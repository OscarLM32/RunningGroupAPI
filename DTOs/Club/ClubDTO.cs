using RunningGroupAPI.Data.Enum;

namespace RunningGroupAPI.DTOs.Club;

public class ClubDTO
{
	public string Id { get; set; }

	public string Title { get; set; }
	public string Description { get; set; }
	public string Image { get; set; }

	public string Country { get; set; }
	public string City { get; set; }
	public string? Address { get; set; }

	public ClubCategory ClubCategory { get; set; }
}