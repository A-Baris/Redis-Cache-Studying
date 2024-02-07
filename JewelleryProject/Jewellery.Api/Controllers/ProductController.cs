
using Jewellery.BLL.Redis_Cache.Abstract;
using Jewellery.Dal.JewelleryContext;
using Jewellery.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jewellery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductCacheService _productCache;

        public ProductController(IProductCacheService productCache)
        {
          _productCache = productCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {

            var products = await _productCache.GetAll();

            

            return Ok(products);
        }

        [HttpGet("getproduct/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productCache.GetbyId(id);
            if (product!=null)
            {
                return Ok(product);
            }
            else return NotFound("Not Exist");
            
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Product entity)
        {

            if(ModelState.IsValid)
            {
                await _productCache.Create(entity);
                return Ok(entity);
                
            }
           
            else { return BadRequest("Failed"); }
            
        }
        [HttpPost("update")]
        public async Task<IActionResult> Update(Product entity)
        {
            await _productCache.Update(entity);
            return Ok(entity);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            var result =await _productCache.Delete(id);
            if(result==true)
            {
                return Ok("Success");
            }
            else { return BadRequest("Fail"); }
            
        }
    }
    }
