using Microsoft.AspNetCore.Identity;

namespace Friendly.WebAPI.PasswordValidator
{
    public class EmailPasswordValidator : IPasswordValidator<Database.User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<Database.User> manager, Database.User user, string password)
        {
            if (string.Equals(user.UserName, password, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "UsernameAsPassword",
                    Description = "You cannot use your email as your password"
                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
