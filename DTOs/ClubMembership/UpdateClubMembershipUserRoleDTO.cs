using System.ComponentModel.DataAnnotations;
using RunningGroupAPI.Data.Enum;

namespace RunningGroupAPI.DTOs.ClubMembership;


public class UpdateClubUserRoleDTO
{
	[Required]
	public string ClubId { get; set; }
	[Required]
	public string UserId { get; set; }
	
	[Required]
	public ClubRole Role { get; set; }
}