using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.HobbyCategory
{
    public class CreateHobbyCategoryRequest
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
