using AutoMapper;
using Friendly.Model.Requests;
using Friendly.Model.Requests.User;

namespace Friendly.WebAPI.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UpdateUserRequest, Database.User>();
            CreateMap<UserRegisterRequest, Database.User>();
        }
    }
}
