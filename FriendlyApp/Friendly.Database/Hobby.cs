using System.ComponentModel.DataAnnotations;

namespace Friendly.Database
{
    public class Hobby : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        public virtual int HobbyCategoryId { get; set; }
        public virtual HobbyCategory HobbyCategory { get; set; }

        public IList<UserHobby> UserHobbies { get; set; }
    }
}
    