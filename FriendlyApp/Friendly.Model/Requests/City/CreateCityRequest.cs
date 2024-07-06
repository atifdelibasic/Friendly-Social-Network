using System;
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.City
{
    public class CreateCityRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required]
        public int CountryId { get; set; }
    }
}
