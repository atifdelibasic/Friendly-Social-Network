using AutoMapper;
using Friendly.Database;

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

        public virtual IEnumerable<T> Get(TSearch search)
        {
            throw new NotImplementedException();
        }

        public virtual T GetById(int id)
        {
            throw new NotImplementedException();
        }


    }
}
