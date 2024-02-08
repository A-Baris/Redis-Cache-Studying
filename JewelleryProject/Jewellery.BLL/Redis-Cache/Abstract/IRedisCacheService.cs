using Jewellery.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.BLL.Redis_Cache.Abstract
{
    public interface IRedisCacheService<T> where T : BaseEntity
    {
        Task<T> SetEntity(T entity);
        Task<T> GetEntityById(int id);
        Task<IEnumerable<T>> GetAllEntities();
        Task<bool> Delete(int id);
        Task<T> Update(int updatedId, T newEntity);
    }
}
