using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.HobbyCategory
{
    public class CreateHobbyCategoryRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
