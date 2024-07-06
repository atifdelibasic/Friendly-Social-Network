using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Hobby
{
    public class UpdateHobbyRequest
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
        public int HobbyCategoryId { get; set; }
    }
}
