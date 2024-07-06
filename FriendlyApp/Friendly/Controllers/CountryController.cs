using Friendly.Model.Requests.Country;
using Friendly.Model.SearchObjects;
using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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

        [HttpGet("/mobile/countries")]
        public async Task<IActionResult> GetCountries([FromQuery] SearchCountryObject request)
        {
            var result = await _countryService.Get(request);
            List<Model.Country> countries = new List<Model.Country>();

            foreach (var item in result.Result)
            {
                if (item.DeletedAt == null)
                {
                    countries.Add(item);
                }
            }

            result.Result = countries;

            return Ok(result);
        }
    }
}
