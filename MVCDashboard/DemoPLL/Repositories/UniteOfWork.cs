using DemoBll.Interfaces;
using DemoBll.Repositories;
using DemoDAl.Contexts;
using DemoPLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoPLL.Repositories
{
	public class UniteOfWork : IUniteOfWork
	{
		private readonly MVCContext context;

		public IEmployeeRepository EmployeeRepository { get; set; }
		public IDepartmentRepository DepartmentRepository { get; set; }
		public UniteOfWork(MVCContext _context)
		{
			EmployeeRepository = new EmployeeRepository(_context);
			DepartmentRepository = new DepartmentRepository(_context);
			context = _context;
		}

		public async Task<int> Complete()
		{
			return await context.SaveChangesAsync();
		}

		public void Dispose()
		{
			context.Dispose();		
		}
		// ~UniteOfWork()
		//{
		//	context.Dispose();
		//}
	}
}
