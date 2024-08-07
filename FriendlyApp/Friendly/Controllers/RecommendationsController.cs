﻿using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecommendationsController : ControllerBase
    {
        private readonly IRecommenderService _recommenderService;
        private readonly IHobbyService _hobbyService;

        public RecommendationsController(IRecommenderService recommenderService, IHobbyService hobbyService)
        {
            _recommenderService = recommenderService;
            _hobbyService = hobbyService;
        }


        [HttpGet("predict")]
        public async Task<IActionResult> Predict(int userId)
        {

            var recommended = _recommenderService.GetRecommendedHobbiesForUser(userId);

            var hobbies = await _hobbyService.GetHobbiesByIds(recommended);

            return Ok(hobbies);
        }
    }
}
