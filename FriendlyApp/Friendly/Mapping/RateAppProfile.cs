using AutoMapper;
using Friendly.Model.Requests.RateApp;

namespace Friendly.WebAPI.Mapping
{
    public class RateAppProfile : Profile
    {
        public RateAppProfile()
        {
            CreateMap<CreateRateAppRequest, Database.RateApp>();
            CreateMap<UpdateRateAppRequest, Database.RateApp>();
            CreateMap<Database.RateApp, Model.RateApp>();
        }
    }
}