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

        [HttpGet("friends")]
        public async Task<IActionResult> GetFriendsPosts([FromQuery]SearchPostRequest request)
        {
            var userId = Convert.ToInt32(User.FindFirst("userid").Value);
            request.UserId = userId;

            var posts = await _postService.GetFriendsPosts(request);

            return Ok(posts);
        }

        [HttpGet("nearby")]
        public async Task<IActionResult> GetNearbyPosts([FromQuery] SearchNearbyPostsRequest request)
        {
            //var userId = Convert.ToInt32(User.FindFirst("userid").Value);

            var posts = await _postService.GetNearbyPosts(request);

            return Ok(posts);
        }
    }
}
