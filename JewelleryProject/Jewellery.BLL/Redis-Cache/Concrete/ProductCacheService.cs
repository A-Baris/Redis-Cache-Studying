using Jewellery.BLL.Redis_Cache.Abstract;
using Jewellery.Dal.JewelleryContext;
using Jewellery.Dal.Redis_Cache;
using Jewellery.Dal.Redis_Cache.Concrete;
using Jewellery.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.BLL.Redis_Cache.Concrete
{
    public class ProductCacheService : CacheWithRepository<Product>, IProductCacheService
    {
        public ProductCacheService(int dbNo, string entityKey, string url, ProjectContext context) : base(dbNo, entityKey, url, context)
        {
        }
    }
}
