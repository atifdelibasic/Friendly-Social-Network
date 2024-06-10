using Friendly.Model.SearchObjects;

namespace Friendly.Model.Requests.Feedback
{
    public class SearchFeedbackRequest:BaseOffsetSearchObject
    {
        public string Text { get; set; }
        public int UserId { get; set; }
    }
}
