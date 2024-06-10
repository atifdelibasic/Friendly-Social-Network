
namespace Friendly.Model.SearchObjects
{
    public class SearchCityRequest : BaseOffsetSearchObject
    {
        public int? CountryId { get; set; }
        public string Text { get; set; }
    }
}
