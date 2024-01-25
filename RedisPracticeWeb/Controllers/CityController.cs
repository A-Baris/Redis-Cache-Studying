using Microsoft.AspNetCore.Mvc;
using Redis.Cache.Abstract;
using Redis.Cache.Entities;

namespace RedisPracticeWeb.Controllers
{
    public class CityController : Controller
    {
        private readonly ICacheService<City> _cacheService;

        public CityController(ICacheService<City> cacheService)
        {
            _cacheService = cacheService;
        }

        public IActionResult AddCity(string id,string name)
        {
            City city = new City() { Id=id,Name = name };
            _cacheService.SetValue(city);
            return Ok($"Şehir eklendi : {name}");
        }

        public IActionResult GetCity(string key)
        {
            var city = _cacheService.GetValue(key);
            return Ok(city);
        }

        public IActionResult GetAllCities()
        {
            var cityList = _cacheService.GetAll();
            return View(cityList);
        }
        public IActionResult DeleteCity(string id)
        {
            var result = _cacheService.DeleteValue(id);
            if (result) return Ok("İşlem gerçekleşti");
            return BadRequest("İşlem başarısız");
        }
    }
}
