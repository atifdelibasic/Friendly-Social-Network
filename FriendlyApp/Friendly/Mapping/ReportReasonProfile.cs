using AutoMapper;

namespace Friendly.WebAPI.Mapping
{
    public class ReportReasonProfile : Profile
    {
        public ReportReasonProfile()
        {
            CreateMap<Database.ReportReason, Model.ReportReason>();
        }
    }
}
