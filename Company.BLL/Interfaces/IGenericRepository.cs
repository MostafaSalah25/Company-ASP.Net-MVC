using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IGenericRepository<T>
    {

        // 5 Signatures for Methods of CRUD Operations ...   methods Asynchronous way > async & await
        Task<T> Get(int? id);
        Task<IEnumerable<T>> GetAll();
        Task<int> Add(T T);
        Task<int> Delete(T T);
        Task<int> Update(T T);

    }
}
