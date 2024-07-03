using Microsoft.AspNetCore.Mvc;
using Friendly.Model.Requests.HobbyCategory;
using Friendly.Service;
using Microsoft.AspNetCore.Authorization;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class HobbyCategoryController : BaseCRUDController<Model.HobbyCategory, SearchHobbyCategoryRequest, CreateHobbyCategoryRequest, UpdateHobbyCategoryRequest>
    {
        private readonly IHobbyCategoryService _hobbyCategoryService;
        public HobbyCategoryController(IHobbyCategoryService service) : base(service)
        {
            _hobbyCategoryService = service;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _hobbyCategoryService.DeleteHobbyCategory(id);

            return Ok();
        }
    }
}
