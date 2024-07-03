
using Friendly.Model.Requests.Stats;

namespace Friendly.Model
{
    public class StatsDashboardModel
    {
        public int TotalPostsCount { get; set; }
        public int TotalRateAppCount { get; set; }
        public int TotalFeedbackCount { get; set; }
        public int TotalReportCount { get; set; }

        public int TotalPostsTodayCount { get; set; }
        public int TotalReportCountToday { get; set; }
        public int TotalFeedbackCountToday { get; set; }
        public int TotalRateAppCountToday { get; set; }

        public int TotalUsersCount { get; set; }
        public int DeletedUsersCount { get; set; }
        public int TotalUsersTodayCount { get; set; }
        public List<UserPostCount> GetTopActiveUsers { get; set; }
        public double PostGrowthRate { get; set; }
        public double AllTimeAppRating { get; set; }

    }
}
