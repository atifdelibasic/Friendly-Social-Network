using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Role
{
    public class GetRoleByIdRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
