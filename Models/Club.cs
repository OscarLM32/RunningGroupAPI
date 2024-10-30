using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RunningGroupAPI.Data.Enum;

namespace RunningGroupAPI.Models;

public class Club
{
	[Key]
	public int Id { get; set; }
	
	public string? Title { get; set; }
	public string? Description { get; set; }
	public string? Image { get; set; }
	
	public string Country { get; set; }
	public string City { get; set; }
	public string? Address { get; set; }
	
	public ClubCategory ClubCategory { get; set; }
	
	[ForeignKey("AppUser")]
	public string? AppUserId { get; set; }
	public AppUser? AppUser { get; set; }
}
