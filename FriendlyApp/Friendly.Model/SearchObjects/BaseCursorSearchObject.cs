using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Friendly.Model.SearchObjects
{
    public class BaseCursorSearchObject
    {
        [DefaultValue(15)]
        [Range(0, 100, ErrorMessage = "Limit must be between 0 and 100")]
        public int Limit { get; set; }

        [DefaultValue(null)]
        public int? Cursor { get; set; }
    }
}
