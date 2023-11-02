using AutoMapper;
using Friendly.Model;
using Friendly.Model.Requests.Post;
using Microsoft.EntityFrameworkCore;

namespace Friendly.Service
{
    public class PostService : BaseCRUDService<Model.Post, Database.Post, SearchPostRequest, CreatePostRequest, UpdatePostRequest>, IPostService
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

        public async Task<List<Model.Post>> GetFriendsPosts(SearchPostRequest request)
        {

            var friendsIds = _context.Friendship
                .Where(f => (f.UserId == request.UserId || f.FriendId == request.UserId) && f.Status == Database.FriendshipStatus.Friends)
                .Select(f => f.UserId == request.UserId ? f.FriendId : f.UserId);


            var query = _context.Post
                .Where(p => friendsIds.Contains(p.UserId) || p.UserId == request.UserId)
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
                .Take(request.Limit)
                .AsNoTracking();

            if (request.Cursor.HasValue)
            {
                query = query.Where(p => p.Id > request.Cursor.Value);
            }

            var posts = await query.ToListAsync();

            return _mapper.Map<List<Model.Post>>(posts);
        }

        public async Task<List<Model.Post>> GetNearbyPosts(SearchNearbyPostsRequest request)
        {

            var point = new { lat = request.Latitude, lon = request.Longitude };

            var query =  _context.Post
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
                    ) <= 10)
                )
                .Where(p => p.DeletedAt == null);
               

            if (request.Cursor.HasValue)
            {
                query = query.Where(p => p.Id > request.Cursor.Value);
            }

            var posts = await query.Take(request.Limit).ToListAsync();

            return _mapper.Map<List<Model.Post>>(posts);
        }
    }
}
