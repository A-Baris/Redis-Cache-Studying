
using Jewellery.Dal.Redis_Cache;
using Jewellery.Dal.Redis_Cache.Abstract;
using Jewellery.Dal.Redis_Cache.Concrete;
using Jewellery.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jewellery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryCacheService _categoryCache;


        public CategoryController(ICategoryCacheService categoryCache)
        {

            _categoryCache = categoryCache;

        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Category entity)
        {
            await _categoryCache.Create(entity);


            //await _categoryService.Create(entity);
            return Ok(entity);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryCache.GetAll();
            return Ok(categories);
        }


    }
}
