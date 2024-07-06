using Friendly.Model.Requests.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using publisher_api.Services;
using System.ComponentModel;
using System.Text.Json;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    public class RabbitMqController :ControllerBase
    {
        private readonly IMessageService _msgService;
        public RabbitMqController(IMessageService msgService)
        {
            _msgService = msgService;
        }

        // this api will simulate microeervices behaviour, it will hit rabbit mq which will hit this web api back 
        // in real world scenario we would have for example an api gateway which hits another micrservices for read/write operations and so on
        // communication can be over http, grpc..

        // api will hit rabbit mq which will hit report controller back (create controller endpoint) :D 
        [HttpPost("produce-message")]
        public IActionResult ProduceMessage([FromBody] CreateReportRequest request)
        {
            Console.WriteLine("produce message started on web api...");

            string token = HttpContext.Request.Headers["Authorization"];

            Report report = new Report
            {
                PostId = request.PostId,
                CommentId = request.CommentId,
                ReportReasonId = request.ReportReasonId,
                AdditionalComment = request.AdditionalComment,
                Token = token
            };

            string jsonString = JsonSerializer.Serialize(report);

            _msgService.Enqueue(jsonString);

            return Ok();
        }

        private class Report
        {
            [DefaultValue(null)]
            public int? PostId { get; set; }

            [DefaultValue(null)]
            public int? CommentId { get; set; }
            public int ReportReasonId { get; set; }
            public string AdditionalComment { get; set; }
            public string Token { get; set; }
        }
    }
}
