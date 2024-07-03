using Microsoft.AspNetCore.Mvc;
using Friendly.Service;
using Friendly.Model.Requests.Like;
using Microsoft.AspNetCore.Authorization;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class LikeController: ControllerBase
    {
        private readonly ILikeService _service;

        public LikeController(ILikeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetLikes([FromQuery]SearchLikesRequest search = null)
        {
            List<Model.Like> likes = await _service.GetLikes(search);

            return Ok(likes);
        }

        [HttpPost]
        public async Task<IActionResult> Like(CreateLikeRequest request)
        {
            Model.Like like = await _service.Like(request);

            return Ok(like);
        }
    }
}
