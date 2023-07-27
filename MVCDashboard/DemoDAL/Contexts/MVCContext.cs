using DemoDAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDAl.Contexts
{
    public class MVCContext:IdentityDbContext<ApplicationUser>
    {
        public MVCContext(DbContextOptions<MVCContext>options):base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("Server=.;Database=MvcDb1;Trusted_Connection=true;");/*MultipleActiveResultSets=true*/
        public DbSet<Department> Departments { get; set; }
        public DbSet<Empployee> Employees { get; set; }
    }
}
