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
        public ReportController(IReportService service) : base(service)
        {
        }
    }
}
