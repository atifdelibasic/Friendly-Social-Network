namespace Friendly.Service
{
    public interface ICRUDService<T, TSearch, TInsert, TUpdate> : IReadService<T, TSearch> where T : class where TSearch : class where TInsert : class where TUpdate : class
    {
        public T Insert(TInsert request);
        public T Update(int id, TUpdate request);
    }
}
