using Friendly.Model.Requests.Post;
using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "User")]
    [AllowAnonymous]
    public class PostController : BaseCRUDController<Model.Post, object, CreatePostRequest, object>
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService) : base(postService)
        {
            _postService = postService;
        }

        [JwtFilter]
        public override async Task<Model.Post> Insert([FromBody] CreatePostRequest request)
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
            request.UserId = userId;

            return await base.Insert(request);
        }

        [JwtFilter]
        [HttpGet("friends/{take}")]
        public async Task<IActionResult> GetFriendsPosts(int take = 10, int ?lastPostId = null)
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);

            var posts = await _postService.GetFriendsPosts(userId, take, lastPostId);

            return Ok(posts);
        }

        [JwtFilter]
        [HttpGet("nearby/{skip}/{take}")]
        public async Task<IActionResult> GetNearbyPosts(double longitude, double latitude, int radius, int skip = 0, int take = 10)
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);

            var posts = await _postService.GetNearbyPosts(userId, longitude, latitude,  radius,  skip,  take);

            return Ok(posts);
        }

        [HttpGet("comments")]
        public async Task<IActionResult> GetPostComments(int limit, int cursor, int postId)
        {
            var comments = await _postService.GetPostComments(postId, limit, cursor);

            return Ok(comments);
        }
    }
}
