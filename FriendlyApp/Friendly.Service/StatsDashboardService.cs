using Friendly.Database;
using Friendly.Model;
using Friendly.Model.Requests.Stats;
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
            model.TotalFeedbackCount = await _context.Feedback.CountAsync();
            model.TotalRateAppCount = await _context.RateApp.CountAsync();
            model.TotalReportCount = await _context.Report.CountAsync();

            model.TotalPostsTodayCount = await GetTodaysPostsCount();
            model.TotalUsersTodayCount = await GetTodaysUsersCount();
            model.TotalFeedbackCountToday = await GetTodaysFeedback();
            model.TotalRateAppCountToday = await GetTodaysRateApp();
            model.TotalReportCountToday = await GetTodaysReport();

            model.GetTopActiveUsers = await GetTopActiveUsers();
            model.PostGrowthRate = await GetPostGrowthRate();

            model.AllTimeAppRating = await GetAllTimeAverageRating();
            model.DeletedUsersCount = await _context.Users.IgnoreQueryFilters().Where(x => x.DeletedAt != null).CountAsync();

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

        public async Task<int> GetTodaysRateApp()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var todaysPostsCount = await _context.RateApp
                .Where(post => post.DateCreated >= today && post.DateCreated < tomorrow)
                .CountAsync();

            return todaysPostsCount;
        }

        public async Task<double> GetAllTimeAverageRating()
        {
            var averageRating = await _context.RateApp
                .AverageAsync(rate => rate.Rating); 

            return averageRating;
        }

        public async Task<int> GetTodaysReport()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var todaysPostsCount = await _context.Report
                .Where(post => post.DateCreated >= today && post.DateCreated < tomorrow)
                .CountAsync();

            return todaysPostsCount;
        }

        public async Task<int> GetTodaysFeedback()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var todaysPostsCount = await _context.Report
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

        public async Task<List<UserPostCount>> GetTopActiveUsers()
        {
            var lastWeek = DateTime.Today.AddDays(-7);

            var topActiveUsers = await _context.Users
                .Select(user => new UserPostCount
                {
                    UserId = user.Id,
                    Username = user.FirstName + " " + user.LastName,
                    PostCount = _context.Post.Count(post => post.UserId == user.Id)
                })
                .Where(user => user.PostCount > 0) 
                .OrderByDescending(user => user.PostCount)
                .Take(5)
                .ToListAsync();

            return topActiveUsers;
        }

        public async Task<double> GetPostGrowthRate()
        {
            var lastMonth = DateTime.Today.AddMonths(-1);

            var postsLastMonth = await _context.Post
                .Where(post => post.DateCreated >= lastMonth)
                .CountAsync();

            var postsPreviousMonth = await _context.Post
                .Where(post => post.DateCreated < lastMonth && post.DateCreated >= lastMonth.AddMonths(-1))
                .CountAsync();

            return postsPreviousMonth == 0 ? 0 : (double)(postsLastMonth - postsPreviousMonth) / postsPreviousMonth * 100;
        }

    }
}
