using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Entities;
using System.Collections.Generic;
using System.Linq;


namespace Company.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository // work Non-Generic Rep in service DepartmentRepository
    {
        private readonly CompanyMVCContext context;
        public DepartmentRepository(CompanyMVCContext Context)
        {
            context = Context;
        }
        public int Add(Department department)
        {
            context.Departments.Add(department);
            return context.SaveChanges(); 
        }
        public int Delete(Department department)
        {
           context.Departments.Remove(department);
           return context.SaveChanges();
        }
        public Department Get(int? id)
            => context.Departments.FirstOrDefault(D=> D.Id == id);  
        public IEnumerable<Department> GetAll()
            => context.Departments.ToList(); 
        public int Update(Department department)
        {
            context.Departments.Update(department);
            return context.SaveChanges();
        }

    }
}
