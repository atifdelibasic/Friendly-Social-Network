
namespace Friendly.Model.Requests.Post
{
    public class SearchPostRequest
    {
        public int? Cursor { get; set; }
        public int Limit { get; set; }
        public int UserId { get; set; }
    }
}
