﻿using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class ProfilesController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;
        public ProfilesController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        [HttpGet("{id}/friendship-status")]
        [JwtFilter]
        public async Task<IActionResult> GetFriendshipStatus(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);

            Model.Friendship friendship = await _friendshipService.GetFriendshipStatus(userId, id);

            return Ok(friendship);
        }

        [JwtFilter]
        [HttpGet("friend-requests")]
        public async Task<IActionResult> GetFriendRequests()
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);

            List<Model.Friendship> friendRequests = await _friendshipService.GetFriendRequests(userId);

            return Ok(friendRequests);
        }

        [JwtFilter]
        [HttpPost("{id}/friend-requests")]
        public async Task<IActionResult> SendFriendRequest(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);

            var newFriendRequest =  await _friendshipService.SendFriendRequest(userId, id);


            return Ok(newFriendRequest);
        }

        [HttpPut("/friend-request/{id}/accept")]
        public async Task<IActionResult> AcceptFriendRequest(int id)
        {
            await _friendshipService.AcceptFriendRequest(id);


            return Ok("Friend request declined successfully");
        }

        [HttpPut("/friend-request/{id}/decline")]
        public async Task<IActionResult> DeclineFriendRequest(int id)
        {
            await _friendshipService.DeclineFriendRequest(id);


            return Ok("Friend request declined successfully");
        }
    }
}
