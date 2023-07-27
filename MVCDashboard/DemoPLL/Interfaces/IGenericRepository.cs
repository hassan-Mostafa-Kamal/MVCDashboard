using DemoDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBll.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAll();
		Task<T> Get(int id);
		Task Add(T Item);
		void Update(T Item);
		void Delete(T Item);
	}
}
