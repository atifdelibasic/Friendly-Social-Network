
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Post
{
    public class SearchPostRequest
    {
        public int? Cursor { get; set; }
        [Range(1, 100, ErrorMessage = "Limit must be between 1 and 100")]
        public int Limit { get; set; } = 15;
    }
}
