using Company.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        // 5 Signatures for Meths of CRUD OPs > work Synchronous way  > work Async , await in IGenRep
        Department Get(int? id); 
        IEnumerable<Department> GetAll(); 
        int Add(Department department); 
        int Delete(Department department);
        int Update (Department department);  
    }
}
