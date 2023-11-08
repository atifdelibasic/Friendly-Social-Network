
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.SearchObjects
{
    public class SearchNearbyPostsRequest
    {
        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }

        [DefaultValue(15)]
        [Range(1, 100, ErrorMessage = "Limit must be between 1 and 100")]
        public int Limit { get; set; }

        [DefaultValue(null)]
        public int? Cursor { get; set; }
    }
}
