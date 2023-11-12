using Friendly.Model.SearchObjects;
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Like
{
    public class SearchLikesRequest : BaseCursorSearchObject
    {
        [Required]
        public int PostId { get; set; }
        public string Text { get; set; }
    }
}
