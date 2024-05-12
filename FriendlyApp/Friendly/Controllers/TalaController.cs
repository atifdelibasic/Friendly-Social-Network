using Friendly.Model.Requests.Report;
using Microsoft.AspNetCore.Mvc;
using publisher_api.Services;
using System.Text.Json;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    public class TalaController :ControllerBase
    {
        private readonly IMessageService _msgService;
        public TalaController(IMessageService msgService)
        {
            _msgService = msgService;
        }


        [HttpPost("test")]
        public IActionResult Test1([FromBody] CreateReportRequest request)
        {
            string jsonString = JsonSerializer.Serialize(request);

            Console.WriteLine("posalji poruku");
            Console.WriteLine(jsonString);
            _msgService.Enqueue(jsonString);

            return Ok();
        }
    }
}
