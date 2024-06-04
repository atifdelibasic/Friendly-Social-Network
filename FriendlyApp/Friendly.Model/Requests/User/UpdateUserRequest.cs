
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

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public DateTime DateModified { get; set; } = DateTime.UtcNow;

        public string ProfileImageUrl { get; set; }

        public string ImagePath { get; set; }
        public List<int> HobbyIds { get; set; }
        public string? Description { get; set; }
        public int? CityId { get; set; }
    }
}
