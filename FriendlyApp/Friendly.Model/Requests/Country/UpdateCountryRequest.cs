
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Country
{
    public class UpdateCountryRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
