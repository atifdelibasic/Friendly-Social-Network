using Friendly.Model;
using Friendly.Model.Requests.Report;
using Friendly.Model.SearchObjects;
using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "User,Admin")]
    public class ReportController : BaseCRUDController<Report, SearchReportRequest, CreateReportRequest, UpdateReportRequest>
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService service) : base(service)
        {
            _reportService = service;
        }

        [HttpPost("seen")]
        public async Task<IActionResult> MarkAsSeen([FromQuery]int id)
        {
            await _reportService.MarkAsSeen(id);

            return Ok();
        }
    }
}
