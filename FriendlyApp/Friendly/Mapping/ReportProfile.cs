using AutoMapper;
using Friendly.Model.Requests.Report;

namespace Friendly.WebAPI.Mapping
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<CreateReportRequest, Database.Report>();
            CreateMap<UpdateReportRequest, Database.Report>();
            CreateMap<UpdateReportRequest, Database.Report>();
            CreateMap<Database.Report, Model.Report>();
            CreateMap<Database.ReportReason, Model.ReportReason>();
        }
    }
}
