using AutoMapper;
using Friendly.Model.Requests.Post;
using Friendly.Model;

namespace Friendly.Service
{
    public class PostService:BaseCRUDService<Post, Database.Post, object, CreatePostRequest, object>, IPostService
    {
        public PostService(Database.FriendlyContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
