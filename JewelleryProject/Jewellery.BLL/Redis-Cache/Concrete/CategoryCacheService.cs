using Jewellery.BLL.AbstractRepository;
using Jewellery.BLL.ConcreteRepository;
using Jewellery.BLL.Redis_Cache.Abstract;
using Jewellery.BLL.Redis_Cache.Concrete;
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

    public class CategoryCacheService : RedisCacheService<Category>, ICategoryCacheService
    {
        public CategoryCacheService(int dbNo, string entityKey, string url) : base(dbNo, entityKey, url)
        {
        }
    }
}
