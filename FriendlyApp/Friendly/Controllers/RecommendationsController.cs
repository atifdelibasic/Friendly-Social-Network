using Friendly.Service;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        private readonly IRecommenderService _recommenderService;

        public RecommendationsController(IRecommenderService recommenderService)
        {
            _recommenderService = recommenderService;
        }


        [HttpGet("predict")]
        public IActionResult Predict(int userId, int hobbyId)
        {
            var recommended = _recommenderService.Predict(userId, hobbyId);
            var result = new
            {
                UserId = userId,
                HobbyId = hobbyId,
                Recommended = recommended
            };
            return Ok(result);
        }
    }
}
