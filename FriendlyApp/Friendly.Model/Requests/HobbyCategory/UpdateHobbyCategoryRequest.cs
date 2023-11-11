using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.HobbyCategory
{
    public class UpdateHobbyCategoryRequest
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
