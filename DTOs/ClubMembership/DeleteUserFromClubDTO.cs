using System.ComponentModel.DataAnnotations;

namespace RunningGroupAPI.DTOs.ClubMembership;

public class DeleteUserFromClubDTO
{
	[Required]
	public string ClubId { get; set; }
	[Required]
	public string UserId { get; set; }
}
