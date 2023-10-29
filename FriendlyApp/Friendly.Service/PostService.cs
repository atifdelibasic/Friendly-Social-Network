using AutoMapper;
using Friendly.Model.Requests.Post;
using Microsoft.EntityFrameworkCore;

namespace Friendly.Service
{
    public class PostService : BaseCRUDService<Model.Post, Database.Post, object, CreatePostRequest, UpdatePostRequest>, IPostService
    {
        public PostService(Database.FriendlyContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<Model.Post> Insert(CreatePostRequest request)
        {
            if (!string.IsNullOrEmpty(request.ImagePath))
            {
                byte[] imageBytes = Convert.FromBase64String(request.ImagePath);

                var fileName = $"{Guid.NewGuid()}.jpg";

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                File.WriteAllBytes(path, imageBytes);

                request.ImagePath = fileName;
            }

            return await base.Insert(request);
        }

        public async Task<List<Model.Post>> GetFriendsPosts(int userId, int take, int? lastPostId)
        {

            var friendsIds = _context.Friendship
                .Where(f => (f.UserId == userId || f.FriendId == userId) && f.Status == Database.FriendshipStatus.Friends)
                .Select(f => f.UserId == userId ? f.FriendId : f.UserId);


            var query = _context.Post
                .Where(p => friendsIds.Contains(p.UserId) || p.UserId == userId)
                .Include(p => p.User)
                .Include(p => p.Hobby)
                .Select(p => new Model.Post
                {
                    Id = p.Id,
                    Description = p.Description,
                    ImagePath = p.ImagePath,
                    LikeCount = p.Likes.Count,
                    CommentCount = p.Comments.Count,
                    Hobby = _mapper.Map<Model.Hobby>(p.Hobby),
                    User = _mapper.Map<Model.User>(p.User)
                })
                .OrderByDescending(p => p.Id)
                .Take(take)
                .AsNoTracking();

            if (lastPostId.HasValue)
            {
                query = query.Where(p => p.Id > lastPostId.Value);
            }

            var posts = await query.ToListAsync();

            return _mapper.Map<List<Model.Post>>(posts);
        }

        public async Task<List<Model.Post>> GetNearbyPosts(int userId, double longitude, double latitude, int radius, int skip = 0, int take = 10)
        {

            var point = new { lat = latitude, lon = longitude };

            var nearbyPosts = await _context.Post
                .Include(p => p.User)
                .Include(p => p.Hobby)
                .Where(p => p.Latitude != null && p.Longitude != null)
                .Where(p => (
                     6371 * Math.Acos(
                        Math.Cos((double)(Math.PI * p.Latitude / 180)) *
                        Math.Cos(Math.PI * point.lat / 180) *
                        Math.Cos((double)(Math.PI * point.lon / 180 - Math.PI * p.Longitude / 180)) +
                        Math.Sin((double)(Math.PI * p.Latitude / 180)) *
                        Math.Sin(Math.PI * point.lat / 180)
                    ) <= radius)
                )
                .Where(p => p.DeletedAt == null)
                .Skip(skip)
                .Take(take)
            .ToListAsync();

            return _mapper.Map<List<Model.Post>>(nearbyPosts);
        }
    }
}
