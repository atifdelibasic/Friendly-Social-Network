using AutoMapper;
using Friendly.Model.Requests.FITPassport;

namespace Friendly.WebAPI.Mapping
{
    public class FitPassportProfile : Profile
    {
        public FitPassportProfile()
        {
            CreateMap<CreateFITPassportRequest, Database.FITPassport>();
            CreateMap<UpdateFITPassportRequest, Database.FITPassport>();
            CreateMap<Database.FITPassport, Model.FITPassport>();
        }
    }
}
