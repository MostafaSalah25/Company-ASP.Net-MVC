using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class EmployeeRepository :GenericRepository<Employee>, IEmployeeRepository//work Generic-Repo in service EmpRep
    {

        public CompanyMVCContext Context { get; } 
        public EmployeeRepository(CompanyMVCContext Context) : base(Context)
        {
            this.Context = Context;
        }

        public async Task<IEnumerable<Employee>> SearchEmployee(string value)
            => await Context.Employees.Where(E => E.Name.Contains(value)).ToListAsync();

    }
}
