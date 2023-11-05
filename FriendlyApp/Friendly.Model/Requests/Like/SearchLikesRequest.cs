using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Like
{
    public class SearchLikesRequest
    {
        [Required]
        public int PostId { get; set; }

        [DefaultValue(50)]
        [Range(1, 100, ErrorMessage = "Limit must be between 1 and 100")]
        public int Limit { get; set; }

        public int? Cursor { get; set; }
    }
}
