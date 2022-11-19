using Microsoft.AspNetCore.Mvc;
using Friendly.Model.Requests.HobbyCategory;
using Friendly.Service;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HobbyCategoryController:BaseCRUDController<Model.HobbyCategory, SearchHobbyCategoryRequest, CreateHobbyCategoryRequest, UpdateHobbyCategoryRequest>
    {
        public HobbyCategoryController(IHobbyCategoryService service):base(service)
        {

        }
    }
}
