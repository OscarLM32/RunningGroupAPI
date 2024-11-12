using RunningGroupAPI.Data.Enum;

namespace RunningGroupAPI.DTOs.ClubMembership;

public class ClubMembershipDTO
{
	public string ClubId { get; set; }
	public string UserId { get; set; }
	public ClubRole Role { get; set; }
	public DateTime JoinDate { get; set; }
}