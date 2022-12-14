using AutoMapper;
using Friendly.Database;

namespace Friendly.Service
{
    public class BaseCRUDService<T, TDb, TSearch, TInsert, TUpdate> : BaseReadService<T, TDb, TSearch>, ICRUDService<T, TSearch, TInsert, TUpdate> where T : class where TSearch : class where TInsert : class where TUpdate : class where TDb : class
    {
        public BaseCRUDService(FriendlyContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public virtual T Insert(TInsert request)
        {
            var set = _context.Set<TDb>();

            TDb entity = _mapper.Map<TDb>(request);

            set.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<T>(entity);
        }

        public virtual T Update(int id, TUpdate request)
        {
            var set = _context.Set<TDb>();

            var entity = set.Find(id);

            _mapper.Map(request, entity);
            _context.SaveChanges();

            return _mapper.Map<T>(entity);
        }
    }
}
