
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
            if (ModelState.IsValid)
            {
                await _categoryCache.Create(entity);
                return Ok(entity);

            }

            else { return BadRequest("Failed"); }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryCache.GetAll();
            return Ok(categories);
        }
        [HttpGet("getCategory/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryCache.GetbyId(id);
            if (category != null)
            {
                return Ok(category);
            }
            else return NotFound("Not Exist");

        }
        [HttpPost("update")]
        public async Task<IActionResult> Update(Product entity)
        {
            await _categoryCache.Update(entity);
            return Ok(entity);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryCache.Delete(id);
            if (result == true)
            {
                return Ok("Success");
            }
            else { return BadRequest("Fail"); }

        }

    }
}
