using AutoMapper;
using Friendly.Model.Requests.Hobby;

namespace Friendly.WebAPI.Mapping
{
    public class HobbyProfile : Profile
    {
        public HobbyProfile()
        {
            CreateMap<CreateHobbyRequest, Database.Hobby>();
            CreateMap<UpdateHobbyRequest, Database.Hobby>();

            CreateMap<Database.Hobby, Model.Hobby>();
        }
    }
}
