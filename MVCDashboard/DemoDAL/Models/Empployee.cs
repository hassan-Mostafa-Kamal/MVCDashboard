using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDAL.Models
{
	public class Empployee
	{
		public int Id { get; set; }
		[Required]
		[MaxLength]
		public string Name { get; set; }

		public int? Age { get; set; }
		public string Address { get; set; }


		[DataType(DataType.Currency)]
		public decimal Salary { get; set; }
		public bool IsActive { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public DateTime HireDate { get; set; }
		public DateTime CreationDate { get; set; } = DateTime.Now;
		public int? DepartmentId { get; set; } 
		public Department Department { get; set; }
		public string  ImageName { get; set; }

	}
}
