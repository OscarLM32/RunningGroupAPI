using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using RunningGroupAPI.Data.Enum;

namespace RunningGroupAPI.Models;

public class ClubMembership
{

	[Required]
	public string ClubId { get; set; }	
	[Required]
	public string AppUserId { get; set; }
	
	
	[Required]
	public ClubRole Role { get; set; }


	[Required]
	public DateTime JoinDate { get; set; } = DateTime.UtcNow; // Sets the join date to the current date by default

	public static ModelBuilder ApplyClubMembershipKey(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ClubMembership>()
			.HasKey(cm => new { cm.ClubId, cm.AppUserId });
		return modelBuilder;
	}
	
	
	public ClubMembership(string clubId, string appUserId, ClubRole role = ClubRole.Member)
	{
		ClubId = clubId;
		AppUserId = appUserId;
		Role = role;
		JoinDate = DateTime.UtcNow;
	}
}