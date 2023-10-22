using AutoMapper;
using Friendly.Database;
using Microsoft.EntityFrameworkCore;

namespace Friendly.Service
{
    public class BaseReadService<T, TDb, TSearch> : IReadService<T, TSearch> where T : class where TDb : class where TSearch : class
    {
        protected readonly FriendlyContext _context;
        protected readonly IMapper _mapper;

        public BaseReadService(FriendlyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async virtual Task<IEnumerable<T>> Get(TSearch search)
        {
            var entity = _context.Set<TDb>();

            var list = await entity.ToListAsync();
            return _mapper.Map<List<T>>(list);
        }

        public virtual async Task<T> GetById(int id)
        {
            var entity = await _context.Set<TDb>().FindAsync(id);

            return _mapper.Map<T>(entity);
        }


    }
}
