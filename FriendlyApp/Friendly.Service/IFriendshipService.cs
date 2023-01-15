using Friendly.Database;
using Friendly.Model.Requests.Friendship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
