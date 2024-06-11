
namespace Friendly.Model
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
