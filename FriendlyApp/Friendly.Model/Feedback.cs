
namespace Friendly.Model
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
