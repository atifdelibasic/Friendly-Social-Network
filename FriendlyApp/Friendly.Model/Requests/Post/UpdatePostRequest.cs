
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Post
{
    public class UpdatePostRequest
    {
        [StringLength(maximumLength: 3000, ErrorMessage = "Description is too long")]
        public string Description { get; set; }
    }
}
