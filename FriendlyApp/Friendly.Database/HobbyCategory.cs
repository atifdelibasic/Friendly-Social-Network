using System.ComponentModel.DataAnnotations;

namespace Friendly.Database
{
    public class HobbyCategory : SoftDelete
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Hobby> Hobbies { get; set; }
    }
}
