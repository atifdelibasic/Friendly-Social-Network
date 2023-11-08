using Friendly.Model.SearchObjects;

namespace Friendly.Service
{
    public interface ICRUDService<T, TSearch, TInsert, TUpdate> : IReadService<T, TSearch> where T : class where TSearch : BaseOffsetSearchObject where TInsert : class where TUpdate : class
    {
        public Task<T> Insert(TInsert request);
        public Task<T> Update(int id, TUpdate request);
    }
}
