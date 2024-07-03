
namespace Friendly.Model
{
    public class Hobby
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual int HobbyCategoryId { get; set; }
        public string DeletedAt { get; set; }
        public string DateCreated { get; set; }
    }
}
