
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.City
{
    public class UpdateCityRequest
    {
        [Required]
        public int CountryId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
