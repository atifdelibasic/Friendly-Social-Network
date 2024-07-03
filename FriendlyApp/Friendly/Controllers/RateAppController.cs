using Friendly.Model.Requests.Feedback;
using Friendly.Model.Requests.RateApp;
using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class RateAppController : BaseCRUDController<Model.RateApp, SearchRateAppRequest, CreateRateAppRequest, UpdateRateAppRequest>
    {
        public RateAppController(IRateAppService service) : base(service)
        {

        }
    }
}
