using Friendly.Model.Requests;
using Friendly.Model.Requests.User;
using Friendly.Service;
using Friendly.WebAPI.ActionFilters;
using Friendly.WebAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _service;
        private IConfiguration _configuration;

        public UserController(IUserService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {

            var result = await _service.RegisterUserAsync(request);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {

            var result = await _service.LoginUserAsync(request);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            var result = await _service.ConfirmEmailAsync(userId, token);

            if (result.IsSuccess)
            {
                return Redirect($"{_configuration["AppUrl"]}/confirmemail.html");
            }

            return  NotFound();
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest();

            var result = await _service.ForgotPasswordAsync(email);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordRequest model)
        {

            var result = await _service.ResetpasswordAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles ="Admin,User")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {

            var result = await _service.UpdateUser(id, request);

            if(!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _service.DeleteUser(id);

            if(!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
