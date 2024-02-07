using Jewellery.BLL.AbstractRepository;
using Jewellery.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Dal.Redis_Cache.Abstract
{
    public interface ICategoryCacheService:ICacheWithRepository<Category>
    {
    }
}
