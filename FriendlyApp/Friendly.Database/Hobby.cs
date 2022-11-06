using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Friendly.Database
{
    public class Hobby : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual int HobbyCategoryId { get; set; }
        public virtual HobbyCategory HobbyCategory { get; set; }

        public IList<UserHobby> UserHobbies { get; set; }
    }
}
    