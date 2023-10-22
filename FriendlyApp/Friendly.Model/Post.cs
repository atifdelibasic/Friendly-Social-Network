
namespace Friendly.Model
{
    public class Post
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public User Friend { get; set; }
        public DateTime DateCreated { get; set; }
        public Hobby Hobby { get; set; }
        public string ImagePath { get; set; }
    }
}
