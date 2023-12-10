using AutoMapper;
using Friendly.Database;
using Friendly.Model.SearchObjects;
using Microsoft.EntityFrameworkCore;

namespace Friendly.Service
{
    public class BlockService : IBlockService
    {
        private readonly FriendlyContext _friendlyContext;
        private readonly IMapper _mapper;
        private readonly HttpAccessorHelperService _httpAccessorHelper;

        public BlockService(FriendlyContext friendlyContext, HttpAccessorHelperService httpAccessorHelper, IMapper mapper)
        {
            _friendlyContext = friendlyContext;
            _httpAccessorHelper = httpAccessorHelper;
            _mapper = mapper;
        }

        public async Task BlockUser(int blockedUserId)
        {
            int blockerUserId = _httpAccessorHelper.GetUserId();

            if (!_friendlyContext.Block.Any(b => b.BlockerUserId == blockerUserId && b.BlockedUserId == blockedUserId))
            {
                var block = new Block { BlockerUserId = blockerUserId, BlockedUserId = blockedUserId };
                _friendlyContext.Block.Add(block);
                await _friendlyContext.SaveChangesAsync();
            }
        }

        public async Task UnblockUser(int blockedUserId)
        {
            int blockerUserId = _httpAccessorHelper.GetUserId();

            var block = await _friendlyContext.Block
                .FirstOrDefaultAsync(b => b.BlockerUserId == blockerUserId && b.BlockedUserId == blockedUserId);

            if (block != null)
            {
                _friendlyContext.Block.Remove(block);
                await _friendlyContext.SaveChangesAsync();
            }
        }

        public async Task<List<Model.User>> GetBlockedUsers(SearchBlockedUsersRequest request)
        {
            int blockerUserId = _httpAccessorHelper.GetUserId();

            var query = _friendlyContext.Block
                .Where(b => b.BlockerUserId == blockerUserId);

            if (request.Cursor.HasValue)
            {
                query = query.Where(b => b.BlockedUserId > request.Cursor);
            }

            var blockedUserIds = await query
                .OrderBy(b => b.BlockedUserId)
                .Take(request.Limit)
                .Select(b => b.BlockedUserId)
                .ToListAsync();

            var blockedUsersQuery = _friendlyContext.Users.Where(u => blockedUserIds.Contains(u.Id));
            if (!string.IsNullOrEmpty(request.Text))
            {
                string searchTextLower = request.Text.ToLower();
                blockedUsersQuery = blockedUsersQuery
                    .Where(x => (x.FirstName + " " + x.LastName).ToLower().Contains(request.Text));
            }

            List<Database.User> blockedUsers = await blockedUsersQuery.ToListAsync();

            return _mapper.Map<List<Model.User>>(blockedUsers);
        }
    }
}
