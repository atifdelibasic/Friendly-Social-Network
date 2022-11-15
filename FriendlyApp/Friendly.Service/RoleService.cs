using Friendly.Model;
using Friendly.Model.Requests.Role;
using Microsoft.AspNetCore.Identity;

namespace Friendly.Service
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole<int>> roleManager;
        public RoleService(RoleManager<IdentityRole<int>> roleManager)
        {
            this.roleManager = roleManager;
        }

        public Task<UserManagerResponse> GetById(GetRoleByIdRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UserManagerResponse> CreateRole(CreateRoleRequest request)
        {

            IdentityRole<int> identityRole = new IdentityRole<int>
            {
                Name = request.Name,
                NormalizedName = request.Name.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };

            var role = roleManager.FindByNameAsync(request.Name);
            if (role is not null)
            {
                return new UserManagerResponse
                {
                    Message = "Role already exists.",
                    IsSuccess = false
                };
            }

            IdentityResult result = await roleManager.CreateAsync(identityRole);

            if (!result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "Something went wrong.",
                    IsSuccess = false
                };
            }

            return new UserManagerResponse
            {
                Message = "Role created.",
                IsSuccess = true
            };
        }

        public async Task<UserManagerResponse> UpdateRole(int id, UpdateRoleRequest request)
        {
            var role = await roleManager.FindByIdAsync(id.ToString());

            if (role is null)
            {
                return new UserManagerResponse
                {
                    Message = "Role not found.",
                    IsSuccess = false
                };
            }

            role.Name = request.Name;

            var result = await roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "Something went wrong.",
                    IsSuccess = false
                };
            }

            return new UserManagerResponse
            {
                Message = "Role updated.",
                IsSuccess = true
            };
        }

        public Task<UserManagerResponse> DeleteRole(DeleteRoleRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
