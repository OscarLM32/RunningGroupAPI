using System.ComponentModel.DataAnnotations;
using RunningGroupAPI.Data.Enum;

namespace RunningGroupAPI.DTOs.Club;

public class CreateClubDTO
{
	[Required]
	public string Title { get; set; }
	[Required]
	public string Description { get; set; }

	[Required]
	public string Country { get; set; }
	[Required]
	public string City { get; set; }
	public string? Address { get; set; }

	public ClubCategory ClubCategory { get; set; }

	public IFormFile? Image { get; set; }
	
	[Required]
	public string AppUserOwnerId { get; set; }
	
}