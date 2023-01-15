using AutoMapper;
using Friendly.Database;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace Friendly.Service
{
    public class FriendshipService : IFriendshipService
    {
        private readonly FriendlyContext _friendlyContext;
        private readonly IMapper _mapper;

        public FriendshipService(FriendlyContext friendlyContext, IMapper mapper)
        {
            _friendlyContext = friendlyContext;
            _mapper = mapper;
        }

        protected async Task<Database.Friendship> DbGetFriendshipById(int id)
        {
            var set = _friendlyContext.Set<Database.Friendship>();

            Database.Friendship friendRequest = await set.FindAsync(id);

            return friendRequest;
        }

        public async Task<Model.Friendship> GetFriendshipStatus(int id, int friendId)
        {
            DbSet<Database.Friendship> set = _friendlyContext.Set<Database.Friendship>();

            Database.Friendship friendRequest = await set
                 .Include(x => x.Friend)
                 .SingleOrDefaultAsync(x => (x.UserId == id && x.FriendId == friendId)
                                       || (x.UserId == friendId && x.FriendId == id));

            return _mapper.Map<Model.Friendship>(friendRequest);
        }

        public async Task<List<Model.Friendship>> GetFriendRequests(int id)
        {
            DbSet<Database.Friendship> set = _friendlyContext.Set<Database.Friendship>();

            List<Database.Friendship> friendRequests = await set
                .Where(x => x.FriendId == id && x.Status == Database.FriendshipStatus.Pending)
                .Include(x => x.User)
                .ToListAsync();

            return _mapper.Map<List<Model.Friendship>>(friendRequests);

        }

        public async Task<Model.Friendship> SendFriendRequest(int id, int friendId)
        {
            var set = _friendlyContext.Set<Database.Friendship>();

            Database.Friendship friendRequest = new Database.Friendship
            {
                UserId = id,
                FriendId = friendId,
                Status = Database.FriendshipStatus.Pending
            };

            await set.AddAsync(friendRequest);

            await _friendlyContext.SaveChangesAsync();

            return _mapper.Map<Model.Friendship>(friendRequest);
        }

        public async Task<Model.Friendship> GetFriendshipById(int requestId)
        {

            Database.Friendship friendRequest = await DbGetFriendshipById(requestId);

            return _mapper.Map<Model.Friendship>(friendRequest);
        }

        public async Task<Model.Friendship> AcceptFriendRequest(int id)
        {
            Database.Friendship friendship = await DbGetFriendshipById(id);

            if (friendship == null || friendship.Status != Database.FriendshipStatus.Pending)
            {
                throw new NotFoundException("Friend request does not exist.");
            }


            friendship.Status = Database.FriendshipStatus.Friends;
            friendship.DateCreated = DateTime.Now;

            await _friendlyContext.SaveChangesAsync();
            return _mapper.Map<Model.Friendship>(friendship);
        }

        public async Task<Model.Friendship> DeclineFriendRequest(int id)
        {
            Database.Friendship friendship = await DbGetFriendshipById(id);

            if (friendship == null || friendship.Status != Database.FriendshipStatus.Pending)
            {
                throw new NotFoundException("Friend request does not exist.");
            }

            friendship.DeletedAt = DateTime.Now;

            await _friendlyContext.SaveChangesAsync();

            return _mapper.Map<Model.Friendship>(friendship);
        }
    }
}
