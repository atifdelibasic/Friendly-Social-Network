using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Role
{
    public class DeleteRoleRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
