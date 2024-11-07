
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RunningGroupAPI.Data.Enum;
using RunningGroupAPI.Helpers.Attributes.Validation;

namespace RunningGroupAPI.Models;

public class Race
{
	[Key]
	public int Id { get; set; }
	

	[Required]
	[StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters.")]
	public string Title { get; set; }

	[StringLength(500, ErrorMessage = "Description can't exceed 500 characters.")]
	public string? Description { get; set; }

	[Required]
	[Url(ErrorMessage = "Image must be a valid URL.")]
	public string Image { get; set; }
	

	[Required(ErrorMessage = "RaceCategory is required.")]
	public RaceCategory RaceCategory { get; set; }
	

	[Required(ErrorMessage = "StartDate is required.")]
	public DateTime StartDate { get; set; }

	[Required(ErrorMessage = "EndDate is required.")]
	[DateGreaterThan(nameof(StartDate), ErrorMessage = "EndDate must be later than StartDate.")]
	public DateTime EndDate { get; set; }
	

	// Collection of race participants, each with a role
	public ICollection<RaceParticipant> RaceParticipants { get; set; } = new List<RaceParticipant>();

}