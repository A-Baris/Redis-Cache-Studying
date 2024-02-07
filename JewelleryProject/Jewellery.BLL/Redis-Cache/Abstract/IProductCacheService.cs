using Jewellery.Dal.Redis_Cache;
using Jewellery.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.BLL.Redis_Cache.Abstract
{
    public interface IProductCacheService : ICacheWithRepository<Product>
    {
    }
}
