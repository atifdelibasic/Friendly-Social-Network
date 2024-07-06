using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly IStatsDashboardService _statsDashboardService;
        public StatsController(IStatsDashboardService statsService)
        {
            _statsDashboardService = statsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStats()
        {
            var stats = await _statsDashboardService.GetStats();

            return Ok(stats);
        }
    }
}
