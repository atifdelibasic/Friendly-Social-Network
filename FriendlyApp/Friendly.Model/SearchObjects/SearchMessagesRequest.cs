using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.SearchObjects
{
    public class SearchMessagesRequest : BaseCursorSearchObject
    {
        [Required]
        public int UserId { get; set; }
    }
}
