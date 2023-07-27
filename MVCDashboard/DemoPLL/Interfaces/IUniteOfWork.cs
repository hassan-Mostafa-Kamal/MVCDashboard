using DemoBll.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoPLL.Interfaces
{
	public interface IUniteOfWork: IDisposable
	{
		public IEmployeeRepository EmployeeRepository { get; set; }
		public IDepartmentRepository DepartmentRepository { get; set; }
		Task<int> Complete();
		
	}
}
