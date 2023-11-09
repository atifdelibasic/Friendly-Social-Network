using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.SearchObjects
{
    public class SearchPostRequest : BaseOffsetSearchObject
    {
        [Required]
        public int UserId { get; set; }
    }
}
