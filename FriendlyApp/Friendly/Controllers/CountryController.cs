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
        public CountryController(ICountryService service) : base(service)
        {

        }
    }
}
