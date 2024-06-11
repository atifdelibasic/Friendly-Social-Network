using Friendly.Model.Requests.City;
using Friendly.Model.SearchObjects;
using Friendly.Service;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
    }
}
