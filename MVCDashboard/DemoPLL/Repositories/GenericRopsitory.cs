using DemoBll.Interfaces;
using DemoDAl.Contexts;
using DemoDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBll.Repositories
{
	public class GenericRopsitory<T> : IGenericRepository<T> where T : class
	{
		private readonly MVCContext _dbContext;
		public GenericRopsitory(MVCContext dbContext) 
		{
			//dbContext= new MVCContext();
			_dbContext = dbContext;
		}
		public async Task Add(T Item)
		=> await _dbContext.Set<T>().AddAsync(Item);
		

		public void Delete(T Item)
		=>	_dbContext.Set<T>().Remove(Item);
		

		public async Task<T>  Get(int id)
		{
			//var Dept=_dbContext.Departments.Local.Where(D=>D.Id==id).FirstOrDefault();
			//if(Dept is null) 
			//    Dept = _dbContext.Departments.Where(D => D.Id == id).FirstOrDefault();

			//return Dept;
		
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			if(typeof(T)==typeof(Empployee))
				return  (IEnumerable<T>)await _dbContext.Employees.Include(E=>E.Department).ToListAsync();
			else
			return await _dbContext.Set<T>().ToListAsync();
		}

		public void Update(T item)
		=>_dbContext.Set<T>().Update(item);
		
	}
}
