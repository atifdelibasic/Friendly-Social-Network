using AutoMapper;
using Friendly.Model;
using Friendly.Model.Requests.Report;
using Friendly.Model.SearchObjects;
using Microsoft.EntityFrameworkCore;

namespace Friendly.Service
{
    public class ReportService : BaseCRUDService<Model.Report, Database.Report, SearchReportRequest, CreateReportRequest, UpdateReportRequest>, IReportService
    {
        private readonly HttpAccessorHelperService _httpAccessorHelper;

        public ReportService(Database.FriendlyContext context, IMapper mapper, HttpAccessorHelperService httpAccessorHelper) : base(context, mapper)
        {
            _httpAccessorHelper = httpAccessorHelper;
        }

        public override Task<Report> Insert(CreateReportRequest request)
        {
            int userId = _httpAccessorHelper.GetUserId();
            Database.Report entity = new Database.Report { UserId = userId };

            return ExtendedInsert(request, entity);
        }

        public async override Task<Report> GetById(int id)
        {
            var report = await _context.Report
            .Include(x => x.User)
            .Include(x => x.ReportReason)
            .Include(x => x.Post)  
            .Include(x => x.Comment) 
            .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<Model.Report>(report);
        }

    }
}
