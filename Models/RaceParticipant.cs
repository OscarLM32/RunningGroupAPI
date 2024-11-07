using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using RunningGroupAPI.Data.Enum;

namespace RunningGroupAPI.Models;

public class RaceParticipant
{
	[Required, ForeignKey("AppUser")]
	public string AppUserId { get; set; }
	public AppUser AppUser { get; set; }

	[Required, ForeignKey("Race")]
	public int RaceId { get; set; }
	public Race Race { get; set; }

	public RaceRole Role { get; set; }

	// Composite primary key for (RaceId, AppUserId)
	public static ModelBuilder ApplyRaceParticipantKey(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<RaceParticipant>()
			.HasKey(rp => new { rp.RaceId, rp.AppUserId });
		return modelBuilder;
	}
}