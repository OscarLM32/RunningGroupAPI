using System.ComponentModel.DataAnnotations;

namespace RunningGroupAPI.DTOs.Authentication;

public class LoginDTO
{
	[Required(ErrorMessage = "Email is requiered")]
	[EmailAddress]
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; }
	
	[Required(ErrorMessage = "Password is requiered")]
	[DataType(DataType.Password)]
	public string Password { get; set; }
}