

using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.SearchObjects
{
    public class SearchCommentCursorRequest : BaseCursorSearchObject
    {
        [Required]
        public int PostId { get; set; }
    }
}
