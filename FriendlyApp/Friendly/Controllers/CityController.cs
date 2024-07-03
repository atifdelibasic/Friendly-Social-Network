using Friendly.Model.Requests.City;
using Friendly.Model.SearchObjects;
using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class CityResult<T>
    {
        public int Count { get; set; }
        public List<T> Result { get; set; }
    }

    public class CityController : BaseCRUDController<Model.City, SearchCityRequest, CreateCityRequest, UpdateCityRequest>
    {
        private readonly ICityService _cityService;
        public CityController(ICityService service) : base(service)
        {
            _cityService = service;
        }

        [HttpPut("delete")]
        public async Task<IActionResult> SoftDelete(int id, bool isDeleted)
        {
            await _cityService.SoftDelete(id, isDeleted);

            return Ok();
        }

        [HttpGet("/mobile/cities")]
        public async Task<IActionResult> GetCities([FromQuery] SearchCityRequest request)
        {
            var result = await _cityService.Get(request);
            List<Model.City> cities = new List<Model.City>();

            foreach (var item in result.Result)
            {
                if(item.DeletedAt == null)
                {
                    cities.Add(item);
                }
            }

            result.Result = cities;

            return Ok(result);
        }

    }
}
