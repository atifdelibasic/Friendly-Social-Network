using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Role
{
    public class UpdateRoleRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
    }
}
