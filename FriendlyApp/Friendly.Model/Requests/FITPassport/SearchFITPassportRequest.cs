using Friendly.Model.SearchObjects;

namespace Friendly.Model.Requests.FITPassport
{
    public class SearchFITPassportRequest : BaseOffsetSearchObject
    {
        public string Text { get; set; }
    }
}
