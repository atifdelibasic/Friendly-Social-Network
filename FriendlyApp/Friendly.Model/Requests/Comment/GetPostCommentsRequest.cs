
namespace Friendly.Model.Requests.Comment
{
    public class GetPostCommentsRequest
    {
        public int Cursor { get; set; } // Cursor is CommentId
        public int Limit { get; set; }
        public int PostId { get; set; }
    }
}
