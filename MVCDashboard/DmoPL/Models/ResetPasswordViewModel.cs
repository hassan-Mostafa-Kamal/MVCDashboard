using System.ComponentModel.DataAnnotations;

namespace DmoPL.Models
{
	public class ResetPasswordViewModel
	{


		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "Confirm Password Is Required")]
		[Compare("NewPassword", ErrorMessage = "Confirm Password Dosn't match Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
