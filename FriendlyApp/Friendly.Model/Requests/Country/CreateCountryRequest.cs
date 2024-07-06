
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Country
{
    public class CreateCountryRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
