
namespace Friendly.Model
{
    public class Post
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public User User { get; set; }
        public Hobby Hobby { get; set; }
        public bool IsLikedByUser { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public int UserId { get; set; }
    }
}
