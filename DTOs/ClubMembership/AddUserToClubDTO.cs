using System.ComponentModel.DataAnnotations;
using RunningGroupAPI.Data.Enum;

namespace RunningGroupAPI.DTOs.ClubMembership;

public class AddUserToClubDTO
{
	[Required]
	public int ClubId { get; set; }
	
	[Required]
	public string AppUserId { get; set; }
	
	[Required]
	public ClubRole Role { get; set; } = ClubRole.Member;
	
	
	public AddUserToClubDTO(int clubId, string userId, ClubRole role)
	{
		ClubId = clubId;
		AppUserId = userId;
		Role = role;
	}
}