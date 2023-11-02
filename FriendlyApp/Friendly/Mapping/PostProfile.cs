using AutoMapper;
using Friendly.Model.Requests.Post;

namespace Friendly.WebAPI.Mapping
{
    public class PostProfile:Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostRequest, Database.Post>().ForMember(dest => dest.UserId, opt => opt.Ignore());
            CreateMap<UpdatePostRequest, Database.Post>();
            CreateMap<Database.Post, Model.Post>();

        }
    }
}
