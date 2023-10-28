
namespace Friendly.Service
{
    public interface IFriendshipService
    {
        public Task<Model.Friendship> GetFriendshipStatus(int id, int friendId);
        public Task<List<Model.Friendship>> GetFriendRequests(int id);
        public Task<Model.Friendship> SendFriendRequest(int id, int friendId);
        public Task<Model.Friendship> GetFriendshipById(int requestId);
        public Task<Model.Friendship> AcceptFriendRequest(int id);
        public Task<Model.Friendship> DeclineFriendRequest(int id);
    }
}
