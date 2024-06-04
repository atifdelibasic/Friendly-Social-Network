using AutoMapper;
using Friendly.Model.Requests.Country;

namespace Friendly.WebAPI.Mapping
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<CreateCountryRequest, Database.Country>();
            CreateMap<UpdateCountryRequest, Database.Country>();
            CreateMap<Database.Country, Model.Country>();
        }
    }
}
