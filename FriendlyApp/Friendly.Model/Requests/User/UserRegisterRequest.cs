using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests
{
    public class UserRegisterRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is requireed.")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
