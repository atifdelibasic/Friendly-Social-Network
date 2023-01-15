using AutoMapper;

namespace Friendly.WebAPI.Mapping
{
    public class FriendshipProfile : Profile
    {
        public FriendshipProfile()
        {
            CreateMap<Database.Friendship, Model.Friendship>()
                .ForMember(
                    dest => dest.Friend,
                    opt => opt.MapFrom(src => src.Friend)
                );

            CreateMap<Model.Friendship, Database.Friendship>();
            CreateMap<Model.Requests.Friendship.UpdateFriendshipRequest, Database.Friendship>();

        }
    }
}
