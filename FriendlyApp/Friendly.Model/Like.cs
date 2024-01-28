
namespace Friendly.Model
{
    public class Like
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public User User { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
