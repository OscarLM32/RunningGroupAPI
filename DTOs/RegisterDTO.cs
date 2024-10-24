using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class RegisterDTO
{
	[Required]
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; }
	
	[Required]
	[DataType(DataType.Password)]
	public string Password { get; set; }
	
	[Required]
	[DataType(DataType.Password)]
	[Compare(nameof(Password))]
	public string PasswordConfirmation { get; set; }
}