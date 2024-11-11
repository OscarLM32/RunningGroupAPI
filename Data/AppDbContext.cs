using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RunningGroupAPI.Models;

namespace RunningGroupAPI.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

	public DbSet<AppUser> Users { get; set; }
	public DbSet<Club> Clubs { get; set; }
	public DbSet<Race> Races { get; set; }
	public DbSet<ClubMembership> ClubMemberships { get; set; }
	public DbSet<RaceParticipant> RaceParticipants { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		
		ClubMembership.ApplyClubMembershipKey(modelBuilder);
		RaceParticipant.ApplyRaceParticipantKey(modelBuilder);
	}
}
