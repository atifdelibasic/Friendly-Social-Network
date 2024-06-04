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
        public CityController(ICityService service) : base(service)
        {

        }
    }
}
