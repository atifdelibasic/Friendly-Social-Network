using Friendly.Model.Requests.Like;

namespace Friendly.Service
{
    public interface ILikeService
    {
        public Task<List<Model.Like>> GetLikes(SearchLikesRequest search);
        public Task<Model.Like> Like(CreateLikeRequest search);
    }
}
