
namespace Friendly.Model.Requests.Comment
{
    public class SearchCommentRequest
    {
        public int PostId { get; set; }
        public int Limit { get; set; }
        public int? Cursor { get; set; }
    }
}
