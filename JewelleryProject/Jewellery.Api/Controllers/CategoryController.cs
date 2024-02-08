
using Jewellery.BLL.AbstractRepositories;
using Jewellery.BLL.Redis_Cache.Abstract;
using Jewellery.BLL.Redis_Cache.Concrete;
using Jewellery.Dal.Redis_Cache;
using Jewellery.Dal.Redis_Cache.Abstract;
using Jewellery.Dal.Redis_Cache.Concrete;
using Jewellery.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Jewellery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryCacheService _categoryCache;

        public CategoryController(ICategoryService categoryService,ICategoryCacheService categoryCache)
        {
            _categoryService = categoryService;
            _categoryCache = categoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cacheEntities = await _categoryCache.GetAllEntities();
            if (cacheEntities!=null)
            {
                return Ok(cacheEntities);
            }
            var databaseEntities = await _categoryService.GetAll();
            if(databaseEntities!=null)
            {
                return Ok(databaseEntities);
            }
            return Ok(null);
           
        }
     
       
    }
}
