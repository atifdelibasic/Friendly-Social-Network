
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Post
{
    public class CreatePostRequest
    {
        public int UserId { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        [Required]
        public int HobbyId { get; set; }
        [StringLength(maximumLength: 3000, ErrorMessage = "Description is too long")]
        public string Description { get; set; }
        public string? ImagePath { get; set; }

    }
}
