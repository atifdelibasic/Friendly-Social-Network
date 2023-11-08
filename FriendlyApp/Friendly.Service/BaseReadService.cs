using AutoMapper;
using Friendly.Database;
using Microsoft.EntityFrameworkCore;
using Friendly.Model.SearchObjects;
using Friendly.Model;

namespace Friendly.Service
{
    public class BaseReadService<T, TDb, TSearch> : IReadService<T, TSearch> where T : class where TDb : class where TSearch : BaseOffsetSearchObject
    {
        protected readonly FriendlyContext _context;
        protected readonly IMapper _mapper;

        public BaseReadService(FriendlyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual async Task<PagedResult<T>> Get(TSearch? search = null)
        {
            var query = _context.Set<TDb>().AsQueryable();

            PagedResult<T> result = new PagedResult<T>();

            query = AddFilter(query, search);

            query = AddInclude(query, search);

            result.Count = await query.CountAsync();

            if (search?.Page.HasValue == true && search?.PageSize.HasValue == true)
            {
                query = query.Skip(search.Page.Value * search.PageSize.Value).Take(search.PageSize.Value);
            }

            var list = await query.ToListAsync();

            var tmp = _mapper.Map<List<T>>(list);
            result.Result = tmp;

            return result;
        }

        public virtual IQueryable<TDb> AddFilter(IQueryable<TDb> query, TSearch? search = null)
        {
            return query;
        }

        public virtual IQueryable<TDb> AddInclude(IQueryable<TDb> query, TSearch? search = null)
        {
            return query;
        }

        public virtual async Task<T> GetById(int id)
        {
            var entity = await getById(id);

            return _mapper.Map<T>(entity);
        }

        protected virtual async Task<TDb> getById(int id)
        {
            var entity = await _context.Set<TDb>().FindAsync(id);
            return entity;
        }
    }
}
