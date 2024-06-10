using Friendly.Model.Requests.ReportReason;
using Friendly.Model.SearchObjects;
using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Friendly.WebAPI.Controllers

{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ReportReasonController :ControllerBase
    {
        private readonly IReportReasonService _reportReasonService;
        public ReportReasonController(IReportReasonService reportReasonService)
        {
            _reportReasonService = reportReasonService;
        }
        [HttpGet]
        public async Task<IActionResult> GetReportReasons([FromQuery] ReportReasonRequest request)
        {
            var posts = await _reportReasonService.GetReportReason(request);

            return Ok(posts);
        }
    }
}
