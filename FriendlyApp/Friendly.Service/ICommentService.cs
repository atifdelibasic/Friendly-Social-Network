using Friendly.Model.Requests.Comment;

namespace Friendly.Service
{
    public interface ICommentService: ICRUDService<Model.Comment, SearchCommentRequest, CreateCommentRequest, UpdateCommentRequest>
    {
    }
}
