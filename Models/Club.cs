using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RunningGroupAPI.Data.Enum;

namespace RunningGroupAPI.Models;

public class Club
{
	[Key]
	public string Id { get; set; }
	

	[Required]
	[StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters.")]
	public string Title { get; set; }

	[StringLength(500, ErrorMessage = "Description can't exceed 500 characters.")]
	public string? Description { get; set; }

	[Required]
	[Url(ErrorMessage = "Image must be a valid URL.")]
	public string Image { get; set; }
	

	[Required]
	[StringLength(50, MinimumLength = 2, ErrorMessage = "Country must be between 2 and 50 characters.")]
	public string Country { get; set; }

	[Required]
	[StringLength(50, MinimumLength = 2, ErrorMessage = "City must be between 2 and 50 characters.")]
	public string City { get; set; }

	[StringLength(100, ErrorMessage = "Address can't exceed 100 characters.")]
	public string? Address { get; set; }
	

	[Required(ErrorMessage = "ClubCategory is required.")]
	public ClubCategory ClubCategory { get; set; }

}
