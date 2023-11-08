using Friendly.Model.Requests.Comment;
using Friendly.Model.SearchObjects;

namespace Friendly.Service
{

    public interface ICommentService : ICRUDService<Model.Comment, SearchCommentRequest, CreateCommentRequest, UpdateCommentRequest>
    {
        public Task<Model.Comment> DeleteComment(int id);
        public Task<List<Model.Comment>> GetCommentsCursor(SearchCommentCursorRequest search);

    }
}
