using Friendly.Model.SearchObjects;

namespace Friendly.Model.Requests.HobbyCategory
{
    public class SearchHobbyCategoryRequest : BaseOffsetSearchObject
    {
        public string? Text { get; set; }
    }
}
