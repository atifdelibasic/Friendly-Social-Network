using Friendly.Model.Requests.Feedback;
using Friendly.Model.Requests.RateApp;
using Friendly.Service;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RateAppController : BaseCRUDController<Model.RateApp, SearchRateAppRequest, CreateRateAppRequest, UpdateRateAppRequest>
    {
        public RateAppController(IRateAppService service) : base(service)
        {

        }
    }
}
