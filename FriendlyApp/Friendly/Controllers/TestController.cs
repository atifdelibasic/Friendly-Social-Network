using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController:ControllerBase
    {
        [HttpGet("dobavi")]
        public IActionResult GetData()
        {
            return Ok("alles klar");
        }
    }
}
