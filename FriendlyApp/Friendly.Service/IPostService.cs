using Friendly.Model;
using Friendly.Model.Requests.Comment;
using Friendly.Model.Requests.Post;

namespace Friendly.Service
{
    public interface IPostService : ICRUDService<Post, object, CreatePostRequest, object>
    {
        public Task<List<Model.Post>> GetFriendsPosts(int userId, int take, int? lasPostId);
        public Task<List<Model.Post>> GetNearbyPosts(int userId, double longitude, double latitude, int radius, int skip = 0, int take = 10);
        public Task<List<Model.Comment>> GetPostComments(int postId, int limit = 10, int? cursor = null);
    }
}
