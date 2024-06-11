using Friendly.Model.Requests.City;
using Friendly.Model.Requests.Country;
using Friendly.Model.SearchObjects;
using Friendly.Service;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : BaseCRUDController<Model.Country, SearchCountryObject, CreateCountryRequest, UpdateCountryRequest>
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService service) : base(service)
        {
            _countryService = service;
        }

        [HttpPut("delete")]
        public async Task<IActionResult> SoftDelete(int id, bool isDeleted)
        {
            await _countryService.SoftDelete(id, isDeleted);

            return Ok();
        }
    }
}
