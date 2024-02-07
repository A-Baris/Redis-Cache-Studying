using Jewellery.BLL.AbstractRepository;
using Jewellery.BLL.ConcreteRepository;
using Jewellery.Dal.JewelleryContext;
using Jewellery.Dal.Redis_Cache.Abstract;
using Jewellery.Dal.Redis_Cache.Entities;
using Jewellery.Entity.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Dal.Redis_Cache.Concrete
{

    public class CategoryCacheService : CacheWithRepository<Category>,ICategoryCacheService
    {
        public CategoryCacheService(int dbNo, string entityKey, string url,ProjectContext context) : base(dbNo, entityKey, url,context)
        {
        }
    }
}
