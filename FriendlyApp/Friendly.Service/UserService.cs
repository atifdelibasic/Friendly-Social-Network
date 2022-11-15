using AutoMapper;
using Friendly.Model;
using Friendly.Model.Requests;
using Friendly.Model.Requests.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IMapper _mapper;

        public UserService(UserManager<Database.User> userManager, IConfiguration configuration, IEmailService emailservice, IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailservice;
            _mapper = mapper;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(UserRegisterRequest request)
        {
            var user = _mapper.Map<Database.User>(request);
            user.UserName = user.Email;

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var createdUser = _userManager.FindByEmailAsync(user.Email);
                var role = _userManager.AddToRoleAsync(user, "User");

                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                string url = $"{_configuration["AppUrl"]}/confirmemail?userId={user.Id}&token={validEmailToken}";

                // Send an Email confirmation 
                await _emailService.SendEmailAsync(user.Email, "Confirm your Email", $"<a href='{url}'>Click here to confirm your Email.</a>");

                return new UserManagerResponse
                {
                    Message = "User created successfully",
                    IsSuccess = true
                };
            }

            return new UserManagerResponse
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }


        public async Task<UserManagerResponse> LoginUserAsync(UserLoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return new UserManagerResponse
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

                return new UserManagerResponse
                {
                    Message = tokenAsString,
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "Invalid login credentials.",
                IsSuccess = false
            };
        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "User not found."
                };
            }

            if (user.EmailConfirmed)
                return new UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "Email has been already confirmed."
                };

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "Email confirmed successfully!"
                };
            }

            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "Email not confirmed!",
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return new UserManagerResponse
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

            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "Password reset link has been sent to the email successfully!"
            };
        }

        public async Task<UserManagerResponse> ResetpasswordAsync(ResetPasswordRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                return new UserManagerResponse
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
                return new UserManagerResponse
                {
                    Message = "Password has been reset successfully!",
                    IsSuccess = true
                };
            }

            return new UserManagerResponse
            {
                Message = "Something went wrong",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> UpdateUser(int id, UpdateUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user is null)
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "User not found."
                };
            }


            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.BirthDate = (DateTime)request.BirthDate;
            user.DateModified = request.DateModified;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Something went wrong"
                };
            }

            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "User updated."
            };
        }

        public async Task<UserManagerResponse> DeleteUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user is null)
            {
                return new UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "User not found."
                };
            }

            // Soft deleting user
            user.DeletedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Something went wrong"
                };
            }

            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "User deleted."
            };
        }
    }
}
