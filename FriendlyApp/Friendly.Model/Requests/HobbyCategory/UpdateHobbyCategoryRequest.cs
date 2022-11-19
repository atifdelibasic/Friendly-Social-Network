using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.HobbyCategory
{
    public class UpdateHobbyCategoryRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
