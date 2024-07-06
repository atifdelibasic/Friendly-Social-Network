
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Post
{
    public class UpdatePostRequest
    {
        [StringLength(maximumLength: 3000, ErrorMessage = "Description is too long")]
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }
    }
}
