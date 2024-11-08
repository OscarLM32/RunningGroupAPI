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
	

	public ClubRole Role { get; set; }


	[Required]
	public DateTime JoinDate { get; set; } = DateTime.UtcNow; // Sets the join date to the current date by default


	// Composite primary key for (ClubId, AppUserId)
	public static ModelBuilder ApplyClubMembershipKey(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ClubMembership>()
			.HasKey(cm => new { cm.ClubId, cm.AppUserId });
		return modelBuilder;
	}
}