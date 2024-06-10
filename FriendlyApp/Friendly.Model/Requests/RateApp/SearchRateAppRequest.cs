using Friendly.Model.SearchObjects;

namespace Friendly.Model.Requests.RateApp
{
    public class SearchRateAppRequest:BaseOffsetSearchObject
    {
        public double Rating { get; set; }
        public int UserId { get; set; }
    }
}
