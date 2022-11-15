using Friendly.Model.Requests;
using Friendly.Model.Requests.User;

namespace Friendly.Service
{
    public interface IUserService
    {
        public Task<Model.UserManagerResponse> RegisterUserAsync(UserRegisterRequest request);
        public Task<Model.UserManagerResponse> LoginUserAsync(UserLoginRequest request);
        public Task<Model.UserManagerResponse> ConfirmEmailAsync(string userId, string token);
        public Task<Model.UserManagerResponse> ForgotPasswordAsync(string email);
        public Task<Model.UserManagerResponse> ResetpasswordAsync(ResetPasswordRequest model);
        public Task<Model.UserManagerResponse> UpdateUser(int id, UpdateUserRequest request);
        public Task<Model.UserManagerResponse> DeleteUser(int id);
    }
}
