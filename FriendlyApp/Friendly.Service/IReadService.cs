using Friendly.Model;
using Friendly.Model.SearchObjects;

namespace Friendly.Service
{
    public interface IReadService<T, TSearch> where T : class where TSearch : BaseOffsetSearchObject
    {
        Task<PagedResult<T>> Get(TSearch search = null);
        public Task<T> GetById(int id);
    }
}
