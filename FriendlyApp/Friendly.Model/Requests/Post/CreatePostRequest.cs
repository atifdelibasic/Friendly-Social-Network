
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Post
{
    public class CreatePostRequest
    {
        [DefaultValue(null)]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        public double? Longitude { get; set; }

        [DefaultValue(null)]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
        public double? Latitude { get; set; }

        [Required]
        public int HobbyId { get; set; }

        [StringLength(maximumLength: 3000, ErrorMessage = "Description is too long")]
        public string Description { get; set; }

        [DefaultValue(null)]
        public string? ImagePath { get; set; }

    }
}
