using AutoMapper;
using Friendly.Model.Requests.HobbyCategory;
using Friendly.Model.Requests.Post;

namespace Friendly.WebAPI.Mapping
{
    public class PostProfile:Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostRequest, Database.Post>();
            CreateMap<Database.Post, Model.Post>();

        }
    }
}
