using Friendly.Model.Requests.Role;
using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    public class RoleController:ControllerBase
    {
        private IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateRoleRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _roleService.CreateRole(request);

            if(!result.IsSuccess)
            {
                return BadRequest(result);
            }


            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _roleService.UpdateRole(request);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }


            return Ok(result);
        }
    }
}
