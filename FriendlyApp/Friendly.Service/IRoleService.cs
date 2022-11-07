using Friendly.Model.Requests.Role;

namespace Friendly.Service
{
    public interface IRoleService
    {
        public Task<Model.UserManagerResponse> CreateRole(CreateRoleRequest request);
        public Task<Model.UserManagerResponse> UpdateRole(UpdateRoleRequest request);
        public Task<Model.UserManagerResponse> DeleteRole(DeleteRoleRequest request);
        public Task<Model.UserManagerResponse> GetById(GetRoleByIdRequest request);


    }
}
