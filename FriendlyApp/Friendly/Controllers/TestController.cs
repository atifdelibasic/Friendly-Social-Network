using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("GetData")]
        [Authorize]
        public IActionResult GetData()
        {
            string userId = User.FindFirst(ClaimTypes.Email)?.Value;

            Console.WriteLine(userId);

            return Ok(userId + "result");
        }
    }
}
