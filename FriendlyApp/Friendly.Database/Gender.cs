using System.ComponentModel.DataAnnotations;

namespace Friendly.Database
{
    public class Gender : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
