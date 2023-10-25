using AutoMapper;
using Friendly.Model.Requests.Like;

namespace Friendly.WebAPI.Mapping
{
    public class LikeProfile : Profile
    {
        public LikeProfile()
        {
            CreateMap<SearchLikesRequest, Database.Like>();
            CreateMap<Database.Like, Model.Like>();
        }
    }
}
