using Microsoft.AspNetCore.Identity;

namespace Friendly.Database
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? About { get; set; }
        public DateTime? DeletedAt { get; set; }

        public IList<UserHobby> UserHobbies { get; set; }

        public virtual int? GenderId { get; set; }
        public virtual Gender Gender { get; set; }

        public string Description { get; set; }

    }
}
