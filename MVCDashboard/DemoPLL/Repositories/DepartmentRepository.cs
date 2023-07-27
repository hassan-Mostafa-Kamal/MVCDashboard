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
    public class DepartmentRepository :GenericRopsitory<Department>, IDepartmentRepository
    {
		public DepartmentRepository(MVCContext dbContext) : base(dbContext) { }
	}
}
