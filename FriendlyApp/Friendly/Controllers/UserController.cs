using Friendly.Model.Requests;
using Friendly.Service;
using Friendly.WebAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    public class UserController : ControllerBase
    {
        private UserManager<Database.User> _userManager;
        private IUserService _service;
        private IEmailService _emailService;
        private IConfiguration _configuration;

        public UserController(IUserService service, UserManager<Database.User> userManager, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _service = service;
            _emailService = emailService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetErrMessage();
                return BadRequest(errors);
            }

            var result = await _service.RegisterUserAsync(request);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            Console.Write("dosao u login metodu");
            if (!ModelState.IsValid)
            {
               var errors = ModelState.GetErrMessage();
                return BadRequest(errors);
            }

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
            if (!ModelState.IsValid)
            {
               // var err = ModelState.GetErrMessage();
                return BadRequest();
            }

            var result = await _service.ResetpasswordAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

    }
}
