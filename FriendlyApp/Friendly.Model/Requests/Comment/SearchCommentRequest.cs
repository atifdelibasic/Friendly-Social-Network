
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Comment
{
    public class SearchCommentRequest
    {
        [Required]
        public int PostId { get; set; }

        [Range(1, 100, ErrorMessage = "Limit must be between 1 and 100.")]
        public int Limit { get; set; } = 10;
        public int? Cursor { get; set; } = null;
    }
}
