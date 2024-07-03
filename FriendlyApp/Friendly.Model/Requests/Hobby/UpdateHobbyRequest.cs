using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Hobby
{
    public class UpdateHobbyRequest
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Title { get; set; }

        //[Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Description { get; set; }
    }
}
