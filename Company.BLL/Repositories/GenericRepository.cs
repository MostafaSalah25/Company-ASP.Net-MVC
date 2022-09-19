using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CompanyMVCContext context;
        public GenericRepository(CompanyMVCContext Context)
        {
            context = Context;
        }
        public async Task<int> Add(T item)
        {
            context.Set<T>().Add(item);
            return await context.SaveChangesAsync();
        }
        public async Task<int> Delete(T item)
        {
            context.Set<T>().Remove(item);
            return await context.SaveChangesAsync();
        }
        public async Task<T> Get(int? id)
            => await context.Set<T>().FindAsync(id);
        public async Task<IEnumerable<T>> GetAll()
        {    
            if (typeof(T) == typeof(Employee))
                return (IEnumerable<T>) await context.Set<Employee>().Include(E => E.Department).ToListAsync();
            return await context.Set<T>().ToListAsync();
        }
        public async Task<int> Update(T item)
        {
            context.Set<T>().Update(item);
            return await context.SaveChangesAsync();
        }

    }
}
