using AutoMapper;
using Friendly.Model;
using Friendly.Model.Requests.Comment;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Friendly.Service
{
    public class CommentService: BaseCRUDService<Model.Comment, Database.Comment, SearchCommentRequest, CreateCommentRequest, UpdateCommentRequest>, ICommentService
    {
        public CommentService(Database.FriendlyContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override async Task<IEnumerable<Comment>> Get(SearchCommentRequest search)
        {
            var query = _context.Comment
                .Where(x => x.PostId == search.PostId);

            if (search.CommentId.HasValue)
            {
                query = query.Where(x => x.Id > search.CommentId);
            }

            query = query.OrderBy(x => x.Id).Take(search.Limit);

            var comments = await query.ToListAsync();

            return _mapper.Map<List<Comment>>(comments);
        }
    }
}
