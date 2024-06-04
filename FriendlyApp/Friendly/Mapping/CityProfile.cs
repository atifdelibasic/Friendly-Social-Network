using AutoMapper;
using Friendly.Model.Requests.City;
 
namespace Friendly.WebAPI.Mapping
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CreateCityRequest, Database.City>();
            CreateMap<UpdateCityRequest, Database.City>();
            CreateMap<Database.City, Model.City>();
        }
    }
}
