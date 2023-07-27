using DemoBll.Interfaces;
using DemoDAl.Contexts;
using DemoDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBll.Repositories
{
	public class EmployeeRepository : GenericRopsitory<Empployee>, IEmployeeRepository
	{
		private readonly MVCContext _dbContext;

		public EmployeeRepository(MVCContext dbContext) : base(dbContext)
		{
			//dbContext= new MVCContext();
			_dbContext = dbContext;
		}

		public IQueryable<Empployee> SearchByName(string Name)
		{
			return _dbContext.Employees.Where(E => E.Name.ToLower().Contains(Name.ToLower()));
		}
	}
}
