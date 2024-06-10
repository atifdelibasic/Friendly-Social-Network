
namespace Friendly.Model
{
    public class RateApp
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public User User { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
