using Friendly.Model;
using Friendly.Model.Requests.Post;

namespace Friendly.Service
{
    public interface IPostService : ICRUDService<Post, SearchPostRequest, CreatePostRequest, UpdatePostRequest>
    {
        public Task<List<Model.Post>> GetFriendsPosts(SearchPostRequest request);
        public Task<List<Model.Post>> GetNearbyPosts(int userId, double longitude, double latitude, int radius, int take = 10, int ?cursor = null);
    }
}
