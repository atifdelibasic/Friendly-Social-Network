
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.SearchObjects
{
    public class SearchNearbyPostsRequest : BaseCursorSearchObject
    {
        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }
    }
}
