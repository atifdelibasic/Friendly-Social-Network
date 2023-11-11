using Friendly.Model.SearchObjects;

namespace Friendly.Model.Requests.Hobby
{
    public class SearchHobbyRequest : BaseOffsetSearchObject
    {
        public string? Text { get; set; }
    }
}
