using Friendly.Model.Requests.Post;
using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "User")]
    public class PostController : BaseCRUDController<Model.Post, SearchPostRequest, CreatePostRequest, UpdatePostRequest>
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService) : base(postService)
        {
            _postService = postService;
        }

        public override async Task<Model.Post> Insert([FromBody] CreatePostRequest request)
        {
            var userId = Convert.ToInt32(User.FindFirst("userid").Value);

            request.UserId = userId;

            return await base.Insert(request);
        }

        [HttpGet("friends")]
        public async Task<IActionResult> GetFriendsPosts([FromQuery]SearchPostRequest request)
        {
            var userId = Convert.ToInt32(User.FindFirst("userid").Value);
            request.UserId = userId;

            var posts = await _postService.GetFriendsPosts(request);

            return Ok(posts);
        }

        [HttpGet("nearby/{take}")]
        public async Task<IActionResult> GetNearbyPosts(double longitude, double latitude, int radius, int take = 10, int? cursor = null)
        {
            var userId = Convert.ToInt32(User.FindFirst("userid").Value);

            var posts = await _postService.GetNearbyPosts(userId, longitude, latitude, radius, take, cursor);

            return Ok(posts);
        }
    }
}
