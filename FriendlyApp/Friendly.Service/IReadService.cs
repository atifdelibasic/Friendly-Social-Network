namespace Friendly.Service
{
    public interface IReadService<T, TSearch> where T : class where TSearch : class
    {
        public Task<IEnumerable<T>> Get(TSearch search);
        public T GetById(int id);
    }
}
