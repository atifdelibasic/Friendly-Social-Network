using AutoMapper;
using Friendly.Database;
using Friendly.Model.SearchObjects;

namespace Friendly.Service
{
    public class BaseCRUDService<T, TDb, TSearch, TInsert, TUpdate> : BaseReadService<T, TDb, TSearch>, ICRUDService<T, TSearch, TInsert, TUpdate> where T : class where TSearch : BaseOffsetSearchObject where TInsert : class where TUpdate : class where TDb : class
    {
        public BaseCRUDService(FriendlyContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected virtual async Task<T> ExtendedInsert(TInsert request, TDb entity)
        {
            var set = _context.Set<TDb>();

            _mapper.Map(request, entity);

            set.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<T>(entity);
        }

        public virtual async Task<T> Insert(TInsert request)
        {
            var set = _context.Set<TDb>();

            TDb entity = _mapper.Map<TDb>(request);

            set.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<T>(entity);
        }

        public virtual async Task<T> Update(int id, TUpdate request)
        {
            var set = _context.Set<TDb>();

            var entity = set.Find(id);

            _mapper.Map(request, entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<T>(entity);
        }
    }
}
