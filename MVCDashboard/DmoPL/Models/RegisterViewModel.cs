using System.ComponentModel.DataAnnotations;

namespace DmoPL.Models
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "First Name Is Required")]
		public string FName { get; set; }

		[Required(ErrorMessage = "Last Name Is Required")]
		public string LName { get; set; }

		[Required(ErrorMessage ="Email Is Required")]
		[EmailAddress(ErrorMessage ="Invalid Email")]
		public string Email { get; set; }
		
		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password Is Required")]
		[Compare("Password", ErrorMessage ="Confirm Password Dosn't match Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage = "Terms and Condition Is Required")]
		public bool IsAgree { get; set; }
	}
}
