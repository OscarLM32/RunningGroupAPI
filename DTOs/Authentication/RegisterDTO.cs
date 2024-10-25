using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RunningGroupAPI.DTOs;

public class RegisterDTO
{
	[Required(ErrorMessage = "Email is requiered")]
	[EmailAddress]
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; }
	
	[Required(ErrorMessage = "Password is requiered")]
	[MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
	[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must have at least one letter and one number, and be at least 8 characters long")]
	[DataType(DataType.Password)]
	public string Password { get; set; }
	
	[Required(ErrorMessage = "Password confirmation is requiered")]
	[DataType(DataType.Password)]
	[Compare(nameof(Password), ErrorMessage = "The passwords do not match")]
	public string PasswordConfirmation { get; set; }
}