using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using RunningGroupAPI.Data.Enum;

namespace RunningGroupAPI.Models;

public class ClubMembership
{
	[Required, ForeignKey("AppUser")]
	public string AppUserId { get; set; }
	public AppUser AppUser { get; set; }

	[Required, ForeignKey("Club")]
	public int ClubId { get; set; }
	public Club Club { get; set; }

	// Role of the user in the club
	public ClubRole Role { get; set; }

	// Composite primary key for (ClubId, AppUserId)
	public static ModelBuilder ApplyClubMembershipKey(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ClubMembership>()
			.HasKey(cm => new { cm.ClubId, cm.AppUserId });
		return modelBuilder;
	}
}