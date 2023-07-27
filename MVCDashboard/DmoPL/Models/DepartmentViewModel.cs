using DemoDAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace DmoPL.Models
{
	public class DepartmentViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Code Is Required!")]
		public string Code { get; set; }
		[Required(ErrorMessage = "Name Is Required!")]
		public string Name { get; set; }
		public DateTime DateOfCreation { get; set; }
		public ICollection<Empployee> Employees { get; set; } = new HashSet<Empployee>();
	}
}
