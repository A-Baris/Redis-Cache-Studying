using Jewellery.BLL.AbstractRepository;
using Jewellery.BLL.ConcreteRepository;
using Jewellery.Dal.JewelleryContext;
using Jewellery.Dal.Redis_Cache.Abstract;
using Jewellery.BLL.Redis_Cache.Abstract;
using Jewellery.Dal.Redis_Cache.Concrete;
using Jewellery.BLL.Redis_Cache.Concrete;
using Jewellery.Dal.Redis_Cache.Entities;
using Jewellery.Dal.Redis_Cache;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Jewellery.Entity.Entities;
using Jewellery.BLL.AbstractRepositories;

namespace Jewellery.IOC.Container
{
    public class ServiceContainer
    {
        public static void ServiceConfiguration(IServiceCollection services, IConfiguration configuration)
        {
        

           //SqlDatabase
           
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICategoryService, CategoryService>();






            //Redis-cache
            services.AddTransient(typeof(IRedisCacheService<>), typeof(RedisCacheService<>));

            services.AddScoped<ICategoryCacheService, CategoryCacheService>(sp =>
            {
                var dbNo = RedisDatabase.Categories;
                var entityKey = RedisEntityKey.CategoryKey;
                var url = configuration["CacheOptions:Url"];
                return new CategoryCacheService(dbNo, entityKey, url);
            });
          
            

            //services.AddScoped<IProductCacheService, ProductCacheService>(sp =>
            //{
            //    var context = sp.GetRequiredService<ProjectContext>();
            //    var dbNo = RedisDatabase.Products;
            //    var entityKey = RedisEntityKey.ProductKey;
            //    var url = configuration["CacheOptions:Url"];
            //    return new ProductCacheService(dbNo, entityKey, url, context);

            //});
        }
    }
}
