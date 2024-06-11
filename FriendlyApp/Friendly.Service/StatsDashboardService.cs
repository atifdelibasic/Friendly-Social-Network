using Friendly.Database;
using Friendly.Model;
using Microsoft.EntityFrameworkCore;

namespace Friendly.Service
{
    public class StatsDashboardService : IStatsDashboardService
    {
        private readonly FriendlyContext _context;
        public StatsDashboardService(FriendlyContext context)
        {
            _context = context;
        }
        public async Task<StatsDashboardModel> GetStats()
        {
            StatsDashboardModel model = new StatsDashboardModel();

            model.TotalPostsCount = await _context.Post.CountAsync();
            model.TotalUsersCount = await _context.Users.CountAsync();

            model.TotalPostsTodayCount = await GetTodaysPostsCount();
            model.TotalUsersTodayCount = await GetTodaysUsersCount();

            return model;
        }

        public async Task<int> GetTodaysPostsCount()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var todaysPostsCount = await _context.Post
                .Where(post => post.DateCreated >= today && post.DateCreated < tomorrow)
                .CountAsync();

            return todaysPostsCount;
        }

        public async Task<int> GetTodaysUsersCount()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var todaysPostsCount = await _context.Users
                .Where(post => post.DateCreated >= today && post.DateCreated < tomorrow)
                .CountAsync();

            return todaysPostsCount;
        }
    }
}
