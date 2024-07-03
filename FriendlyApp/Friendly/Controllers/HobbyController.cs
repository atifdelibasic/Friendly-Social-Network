using Friendly.Model.Requests.Hobby;
using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class HobbyController : BaseCRUDController<Model.Hobby, SearchHobbyRequest, CreateHobbyRequest, UpdateHobbyRequest>
    {
        private readonly IHobbyService hobbyService;
        public HobbyController(IHobbyService service) : base(service)
        {
            hobbyService = service;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await hobbyService.DeleteHobby(id);

            return Ok();
        }
    }
}
