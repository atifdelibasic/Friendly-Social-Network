using AutoMapper;
using Friendly.Database;
using Friendly.Model;
using Friendly.Model.Requests.Like;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Friendly.Service
{
    public class LikeService : ILikeService
    {
        private readonly IMapper _mapper;
        private readonly FriendlyContext _context;

        public LikeService(FriendlyContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<Model.Like>> GetLikes(SearchLikesRequest search)
        {
            var query = _context.Like
                .Where(x => x.PostId == search.PostId);


            if (search.Cursor.HasValue)
            {
                query = query.Where(x => x.Id > search.Cursor);
            }

            query = query.OrderBy(x => x.Id).Take(search.Limit);

            var likes = await query.ToListAsync();

            return _mapper.Map<List<Model.Like>>(likes);
        }
    }
}
