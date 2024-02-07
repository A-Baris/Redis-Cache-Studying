using Jewellery.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Dal.Redis_Cache
{
    public interface ICacheWithRepository<T> where T : BaseEntity
    {

        Task<IEnumerable<T>> GetAll();
        Task<T> GetbyId(int id);
        Task<string> Create(T entity);
        Task<string> Update(T entity);
        Task<bool> Delete(int id);
    }
}
