using Friendly.Model.SearchObjects;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Comment
{
    public class SearchCommentRequest : BaseOffsetSearchObject
    {
        [Required]
        public int PostId { get; set; }
    }
}
