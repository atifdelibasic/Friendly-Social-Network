using Friendly.Service.Brokers;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{

    [ApiController]
    [Route("controller")]
    public class BookingsController :ControllerBase
    {
        private readonly IMessageProducer _messageProducer;
        public BookingsController(IMessageProducer messageProducer)
        {
            _messageProducer = messageProducer;
        }

        [HttpPost]
        public IActionResult Test(string message)
        {
            _messageProducer.SendingMessage(message);

            return Ok();
        }
    }
}
