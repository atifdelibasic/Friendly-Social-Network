
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Post
{
    public class CreatePostRequest
    {
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        [Required]
        public int HobbyId { get; set; }
        [StringLength(maximumLength: 3000, ErrorMessage = "Description is too long")]
        public string Description { get; set; }
        public string? ImagePath { get; set; }

    }
}
