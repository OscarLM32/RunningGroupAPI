using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace RunningGroupAPI.Models;

public class AppUser : IdentityUser
{
	public int? Pace { get; set; }
	public int? Mileage { get; set; } 
	public string ProfileImageUrl {get; set;}

	public string? Country { get; set; }
	public string? City { get; set; }

	public ICollection<ClubMembership> ClubMemberships { get; set; } = new List<ClubMembership>();
	public ICollection<RaceParticipant> RaceParticipants { get; set; } = new List<RaceParticipant>();

}
