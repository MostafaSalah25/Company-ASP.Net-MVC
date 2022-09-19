using Company.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Contexts
{
    public class CompanyMVCContext : IdentityDbContext<ApplicationUser>
    {
        public CompanyMVCContext(DbContextOptions<CompanyMVCContext> options) :base (options)
        {

        }
        
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
