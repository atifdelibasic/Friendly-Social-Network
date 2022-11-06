﻿using Microsoft.AspNetCore.Identity;
namespace Friendly.Database
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; }
        public DateTime BirthDate { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? About { get; set; }

        public IList<UserHobby> UserHobbies { get; set; }
    }
}
