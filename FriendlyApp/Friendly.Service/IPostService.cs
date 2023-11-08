using Friendly.Model;
using Friendly.Model.Requests.Post;
using Friendly.Model.SearchObjects;

namespace Friendly.Service
{
    public interface IPostService : ICRUDService<Post, SearchPostRequest, CreatePostRequest, UpdatePostRequest>
    {
        public Task<List<Model.Post>> GetFriendsPosts(SearchPostRequest request);
        public Task<List<Model.Post>> GetNearbyPosts(SearchNearbyPostsRequest request);
    }
}
