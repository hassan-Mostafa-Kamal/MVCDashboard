using DemoDAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace DmoPL.Models
{
	public class EmployeeViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Name is required")]
		[MaxLength(50, ErrorMessage = "Max length is 50")]
		[MinLength(5, ErrorMessage = "Min length is 5")]
		public string Name { get; set; }

		[Range(22, 55)]
		public int? Age { get; set; }
		[RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
			ErrorMessage = "Address Must Be like 123-Streat-City-Country")]
		public string Address { get; set; }


		public decimal Salary { get; set; }
		public bool IsActive { get; set; }
		[EmailAddress]
		public string Email { get; set; }
		[Phone]
		public string Phone { get; set; }
		public DateTime HireDate { get; set; }
		public int? DepartmentId { get; set; }
		public Department Department { get; set; }
		public string ImageName { get; set; }

		public IFormFile Image { get; set; }
	}
}
