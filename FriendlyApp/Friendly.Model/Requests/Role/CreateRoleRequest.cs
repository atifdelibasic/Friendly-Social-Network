using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Role
{
    public class CreateRoleRequest
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
    }
}
