using Friendly.Model.Requests.Hobby;
using Friendly.Service;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HobbyController : BaseCRUDController<Model.Hobby, SearchHobbyRequest, CreateHobbyRequest, UpdateHobbyRequest>
    {
        public HobbyController(IHobbyService service) : base(service)
        {
        }
    }
}
