using AutoMapper;
using Friendly.Database;
using Friendly.Model;
using Friendly.Model.Requests;
using Friendly.Model.Requests.Friendship;
using Friendly.Model.Requests.User;
using Friendly.Model.SearchObjects;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Friendly.Service
{
    public class UserService : BaseReadService<Model.User, Database.User, SearchUserRequest>, IUserService
    {
        private UserManager<Database.User> _userManager;
        private IConfiguration _configuration;
        private IEmailService _emailService;
        private readonly IMapper _mapper;

        public UserService(Database.FriendlyContext context, UserManager<Database.User> userManager, IConfiguration configuration, IEmailService emailservice, IMapper mapper) : base(context, mapper)
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
            user.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var createdUser = _userManager.FindByEmailAsync(user.Email);
                var role = _userManager.AddToRoleAsync(user, "User");

                //var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                //var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                //var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                //string url = $"{_configuration["AppUrl"]}/user/confirmemail?userId={user.Id}&token={validEmailToken}";

                // Send an Email confirmation 
               // var jobId = BackgroundJob.Enqueue(() => _emailService.SendEmailAsync(user.Email, "Confirm your Email", $"<a href='{url}'>Click here to confirm your Email.</a>"));

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

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return new UserManagerResponse
                {
                    Message = "Email is not confirmed",
                    IsSuccess = false
                };
            }


            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                await _userManager.AccessFailedAsync(user);

                if (await _userManager.IsLockedOutAsync(user))
                {

                    return new UserManagerResponse
                    {
                        Message = "The account is locked out",
                        IsSuccess = false
                    };
                }
                return new UserManagerResponse
                {
                    Message = "Invalid login credentials.",
                    IsSuccess = false
                };
            }


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
                //expires: DateTime.Now.AddDays(5),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                User = _mapper.Map<Model.User>(user)
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

        static string TrimUrlPrefix(string url, string prefix)
        {
            // Check if the URL starts with the given prefix and remove it if it does
            if (url.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                return url.Substring(prefix.Length);
            }
            return url;
        }

        public async Task<Model.User> UpdateUser(int id, UpdateUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (!string.IsNullOrEmpty(request.ProfileImageUrl) && request.ProfileImageUrl.Contains("avatar"))
            {
                request.ProfileImageUrl = null;
            } else if(!string.IsNullOrEmpty(request.ProfileImageUrl))
            {
                user.ProfileImageUrl = TrimUrlPrefix(request.ProfileImageUrl, "https://localhost:7169/images/");

            }




            if ( !string.IsNullOrEmpty(request.ImagePath))
            {
                byte[] imageBytes = Convert.FromBase64String(request.ImagePath);

                var fileName = $"{Guid.NewGuid()}.jpg";

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                File.WriteAllBytes(path, imageBytes);

                request.ImagePath = fileName;
                user.ProfileImageUrl = request.ImagePath;
            }


            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.BirthDate = request.BirthDate;
            user.Description = request.Description;
            user.CityId = request.CityId;

            await AddHobby(new AddHobbyRequest { HobbyIds = request.HobbyIds, }, user.Id);
            

            var result = await _userManager.UpdateAsync(user);

            Model.User m = new Model.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Description = request.Description,
                ProfileImageUrl = user.ProfileImageUrl
            };

            return m;
        }

        public async Task<UserManagerResponse> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

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

        public override IQueryable<Database.User> AddFilter(IQueryable<Database.User> query, SearchUserRequest search = null)
        {
            if (!string.IsNullOrEmpty(search.Text))
            {
                string searchTextLower = search.Text.ToLower();
                query = query.Where(x => (x.FirstName + " " + x.LastName).ToLower().Contains(search.Text));
            }

            return base.AddFilter(query, search);
        }

        public async Task<List<Model.User>> GetUsersCursor(SearchUserCursorRequest request)
        {
            var query = _userManager.Users;

            if(!String.IsNullOrEmpty(request.Text))
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(request.Text) || x.LastName.ToLower().Contains(request.Text));
            }

            if (request.Cursor.HasValue)
            {
                query = query.Where(p => p.Id > request.Cursor.Value);
            }

            query = query.OrderBy(p => p.Id)
               .Take(request.Limit)
               .AsNoTracking();

            var users = await query.ToListAsync();

            return _mapper.Map<List<Model.User>>(users);
        }

        public async Task<List<Model.Hobby>> GetUserHobbies(int id)
        {
            var hobbies = await _context.UserHobbies
                .Where(x => x.UserId == id)
                .Include(x => x.Hobby)
                 .Select(uh => uh.Hobby)
                .ToListAsync();

            return _mapper.Map<List<Model.Hobby>>(hobbies);
        }

        public async Task<List<Model.Hobby>> AddHobby(AddHobbyRequest request, int id)
        {
            var user = await _context.Users
               .Include(u => u.UserHobbies)
               .FirstOrDefaultAsync(u => u.Id == id);

            if (request.HobbyIds == null || request.HobbyIds.Count == 0)
            {
                // If HobbyIds is empty, remove all hobbies and return an empty list
                user.UserHobbies.Clear();
                await _context.SaveChangesAsync();

                return new List<Model.Hobby>();
            }

            // Remove hobbies that are not in the list
            var hobbiesToRemove = user.UserHobbies
                .Where(uh => !request.HobbyIds.Contains(uh.HobbyId))
                .ToList();

            foreach (var hobbyToRemove in hobbiesToRemove)
            {
                user.UserHobbies.Remove(hobbyToRemove);
            }

            // Add/create hobbies from the list that do not exist
            var hobbiesToAddIds = request.HobbyIds
                .Where(hId => !user.UserHobbies.Any(uh => uh.HobbyId == hId))
                .ToList();

            var hobbiesToAdd = await _context.Hobby
                .Where(h => hobbiesToAddIds.Contains(h.Id))
                .ToListAsync();

            foreach (var hobby in hobbiesToAdd)
            {
                user.UserHobbies.Add(new UserHobby { User = user, Hobby = hobby });
            }

            await _context.SaveChangesAsync();

            var userHobbies = await GetUserHobbies(id);

            return _mapper.Map<List<Model.Hobby>>(userHobbies);
        }

    }
}
