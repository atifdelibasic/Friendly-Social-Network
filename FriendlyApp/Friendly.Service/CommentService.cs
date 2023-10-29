﻿using AutoMapper;
using Friendly.Model;
using Friendly.Model.Requests.Comment;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace Friendly.Service
{
    public class CommentService: BaseCRUDService<Model.Comment, Database.Comment, SearchCommentRequest, CreateCommentRequest, UpdateCommentRequest>, ICommentService
    {
        public CommentService(Database.FriendlyContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override async Task<Model.Comment> GetById(int id)
        {
            var entity = await _context.Comment.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<Model.Comment>(entity);
        }

        public override async Task<IEnumerable<Comment>> Get(SearchCommentRequest search)
        {
            var query = _context.Comment
                .Where(x => x.PostId == search.PostId);

            if (search.Cursor.HasValue)
            {
                query = query.Where(x => x.Id > search.Cursor);
            }

            query = query
                .Include(x => x.User) 
                .OrderBy(x => x.Id)
                .Take(search.Limit);

            var comments = await query.ToListAsync();

            return _mapper.Map<List<Comment>>(comments);
        }

        public  async Task<Comment> DeleteComment(int id)
        {
            Database.Comment comment = await getById(id);
            if (comment == null)
            {
                throw new NotFoundException("Comment not found");
            }

            comment.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            Model.Comment model = _mapper.Map<Model.Comment>(comment);

            return model;
        }

    }
}
