using Friendly.Model.SearchObjects;
using System;

namespace Friendly.Service
{
    public interface IBlockService
    {
        Task BlockUser(int blockedUserId);
        Task UnblockUser(int blockedUserId);
        Task<List<Model.User>> GetBlockedUsers(SearchBlockedUsersRequest request);
    }
}
