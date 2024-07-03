
namespace Friendly.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DeletedAt { get; set; }

        public string Description { get; set; }
        public City? City { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public List<string> Roles { get; set; }
        public string DateCreated { get; set; }
    } 
}
