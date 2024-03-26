
using Friendly.Model.SearchObjects;

namespace Friendly.Model.Requests.Post
{
    public class GetUserPostsRequest : BaseCursorSearchObject
    {
        public int UserId { get; set; }
    }
}
