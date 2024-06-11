
using Friendly.Model.SearchObjects;

namespace Friendly.Service
{
    public interface IStatsDashboardService
    {
        public Task<Model.StatsDashboardModel> GetStats();

    }
}
