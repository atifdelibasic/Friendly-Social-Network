﻿using Friendly.Model.Requests.Post;
using Friendly.Model.SearchObjects;
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
        public async Task<IActionResult> GetFriendsPosts([FromQuery]BaseCursorSearchObject request)
        {
            var posts = await _postService.GetFriendsPosts(request);

            return Ok(posts);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserPosts([FromQuery] GetUserPostsRequest request)
        {
            var posts = await _postService.GetUserPostsCursor(request);

            return Ok(posts);
        }

        [HttpGet("nearby")]
        public async Task<IActionResult> GetNearbyPosts([FromQuery] SearchNearbyPostsRequest request)
        {
            var posts = await _postService.GetNearbyPosts(request);

            return Ok(posts);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost( int id)
        {
             await _postService.DeletePost(id);

            return Ok();
        }
    }
}
