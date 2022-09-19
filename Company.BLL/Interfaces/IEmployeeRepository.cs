using Company.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {

        Task<IEnumerable<Employee>> SearchEmployee(string value);
    }
}
