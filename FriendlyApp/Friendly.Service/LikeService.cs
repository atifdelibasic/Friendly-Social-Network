﻿using AutoMapper;
using Friendly.Database;
using Friendly.Model.Requests.Like;
using Microsoft.EntityFrameworkCore;

namespace Friendly.Service
{
    public class LikeService : ILikeService
    {
        private readonly IMapper _mapper;
        private readonly FriendlyContext _context;
        private readonly HttpAccessorHelperService _httpAccessorHelper;
        private readonly INotificationService _notificationService;
        private readonly IPostService _postService;

        public LikeService(FriendlyContext context, IMapper mapper, HttpAccessorHelperService httpAccessorHelper, IPostService postService, INotificationService notificationService)
        {
            _mapper = mapper;
            _context = context;
            _httpAccessorHelper = httpAccessorHelper;
            _notificationService = notificationService;
            _postService = postService;
        }

        public async Task<List<Model.Like>> GetLikes(SearchLikesRequest search)
        {
            var query = _context.Like
                .Where(x => x.PostId == search.PostId);

            if (!string.IsNullOrEmpty(search.Text))
            {
                query = query.Where(x => (x.User.FirstName + " " + x.User.LastName).ToLower().Contains(search.Text.ToLower()));
            }

            if (search.Cursor.HasValue)
            {
                query = query.Where(x => x.Id > search.Cursor);
            }

            var likes = await query.Include(x => x.User).OrderBy(x => x.Id).Take(search.Limit).ToListAsync();

            return _mapper.Map<List<Model.Like>>(likes);
        }

        public async Task<Model.Like> Like(CreateLikeRequest request)
        {
            int userId = _httpAccessorHelper.GetUserId();

            var like = await getLike(request.PostId, userId);

            if (like != null)
            {
                await DeleteLike(like);
                return _mapper.Map<Model.Like>(like);
            }

            Model.Post post = await _postService.GetById(request.PostId);
            await _notificationService.CreateNotification(new Model.Requests.Notification.CreateNotificationRequest
            {
                SenderId = userId,
                RecipientId = post.UserId,
                Message = "Liked your post "
            });

            Database.Like entity = new Database.Like { UserId = userId };

            return await CreateLike(request, entity);
        }

        protected async Task<Database.Like> getLike(int postId, int userId)
        {
            var like = await _context.Like.Include(x => x.User).FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);

            return _mapper.Map<Database.Like>(like);
        }

        public async Task<Model.Like> GetLike(int postId, int userId)
        {
            var like = await getLike(postId, userId);

            return _mapper.Map<Model.Like>(like);
        }

        public async Task<Model.Like> CreateLike(CreateLikeRequest request, Database.Like entity)
        {
            _mapper.Map(request, entity);

            _context.Like.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<Model.Like>(entity);
        }

        public async Task DeleteLike(Database.Like like)
        {
            _context.Like.Remove(like);
            await _context.SaveChangesAsync();
        }


    }
}
