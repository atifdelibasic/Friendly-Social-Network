
namespace Friendly.Model.Requests.Post
{
    public class CreatePostRequest
    {
        public int UserId { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public int HobbyId { get; set; }
        public string Description { get; set; }
        public string? ImagePath { get; set; }

    }
}
