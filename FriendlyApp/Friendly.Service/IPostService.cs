using Friendly.Model;
using Friendly.Model.Requests.Post;

namespace Friendly.Service
{
    public interface IPostService : ICRUDService<Post, object, CreatePostRequest, UpdatePostRequest>
    {
        public Task<List<Model.Post>> GetFriendsPosts(int userId, int take, int? lasPostId);
        public Task<List<Model.Post>> GetNearbyPosts(int userId, double longitude, double latitude, int radius, int skip = 0, int take = 10);
    }
}
