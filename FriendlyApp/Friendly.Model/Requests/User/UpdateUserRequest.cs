﻿
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.User
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? BirthDate { get; set; }

        public DateTime DateModified { get; set; } = DateTime.UtcNow;
        
    }
}
