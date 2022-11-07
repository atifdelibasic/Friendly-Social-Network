using Friendly.Model.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Friendly.Service
{
    public class UserService : IUserService
    {
        private UserManager<Database.User> _userManager;
        private IConfiguration _configuration;
        private IEmailService _emailService;

        public UserService(UserManager<Database.User> userManager, IConfiguration configuration, IEmailService emailservice)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailservice;
        }

        public async Task<Model.UserManagerResponse> RegisterUserAsync(UserRegisterRequest request)
        {

            var user = new Database.User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                UserName = request.Email,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                string url = $"{_configuration["AppUrl"]}/confirmemail?userId={user.Id}&token={validEmailToken}";

                // Send an Email confirmation 
                await _emailService.SendEmailAsync(user.Email, "Confirm your Email", $"<a href='{url}'>Click here to confirm your Email.</a>");

                return new Model.UserManagerResponse
                {
                    Message = "User created successfully",
                    IsSuccess = true
                };
            }

            return new Model.UserManagerResponse
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };

        }


        public async Task<Model.UserManagerResponse> LoginUserAsync(UserLoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
        
            if (user is null)
            {
                return new Model.UserManagerResponse
                {
                    Message = "Invalid email or password.",
                    IsSuccess = false
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (result)
            {
                // Get user roles
                var roles = await _userManager.GetRolesAsync(user);

                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim("email", user.Email));
                claims.Add(new Claim("userid", user.Id.ToString()));
                claims.Add(new Claim("firstname", user.FirstName.ToString()));
                claims.Add(new Claim("lastname", user.LastName.ToString()));

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["AuthSettings:ValidIssuer"],
                    audience: _configuration["AuthSettings:ValidAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(5),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

                string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

                return new Model.UserManagerResponse
                {
                    Message = tokenAsString,
                    IsSuccess = true,
                };
            }

            return new Model.UserManagerResponse
            {
                Message = "Invalid login credentials.",
                IsSuccess = false
            };
        }

        public async Task<Model.UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
            {
                return new Model.UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "User not found."
                };
            }

            if (user.EmailConfirmed)
                return new Model.UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "Email has been already confirmed."
                };

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
            {
                return new Model.UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "Email confirmed successfully!"
                };
            }

            return new Model.UserManagerResponse
            {
                IsSuccess = false,
                Message = "Email not confirmed!",
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<Model.UserManagerResponse> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return new Model.UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "Reset password link has been sent."
                };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration["AppUrl"]}/ResetPassword?email={email}&token={validToken}";

            await _emailService.SendEmailAsync(email, "Reset password", $"<div>To reset your password <a href='{url}'>Click here</a></div>");

            return new Model.UserManagerResponse
            {
                IsSuccess = true,
                Message = "Password reset link has been sent to the email successfully!"
            };
        }

        public async Task<Model.UserManagerResponse> ResetpasswordAsync(ResetPasswordRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                return new Model.UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "Reset password link has been sent."
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ResetPasswordAsync(user, normalToken, model.Password);
            if (result.Succeeded)
            {
                return new Model.UserManagerResponse
                {
                    Message = "Password has been reset successfully!",
                    IsSuccess = true
                };
            }

            return new Model.UserManagerResponse
            {
                Message = "Something went wrong",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    }
}
