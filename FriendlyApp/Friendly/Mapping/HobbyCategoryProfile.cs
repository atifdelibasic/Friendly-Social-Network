using AutoMapper;
using Friendly.Model.Requests.HobbyCategory;

namespace Friendly.WebAPI.Mapping
{
    public class HobbyCategoryProfile : Profile
    {
        public HobbyCategoryProfile()
        {
            CreateMap<CreateHobbyCategoryRequest, Database.HobbyCategory>();
            CreateMap<UpdateHobbyCategoryRequest, Database.HobbyCategory>();

            CreateMap<Database.HobbyCategory, Model.HobbyCategory>();
        }
    }
}
