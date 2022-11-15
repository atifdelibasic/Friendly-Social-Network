using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests
{
    public class UserRegisterRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required.")]
       // [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required.")]
       // [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
