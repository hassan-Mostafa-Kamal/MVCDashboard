using DemoDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBll.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Empployee>
    {
		//public IEnumerable<Empployee> GetAll();
		//public Empployee Get(int id);
		//public int Add(Empployee Employee);
		//public int Update(Empployee Employee);
		//public int Delete(Empployee Employee);

		 IQueryable<Empployee> SearchByName(string Name);
	}
}
