using AutoMapper;
using Friendly.Model;
using Friendly.Model.Requests.RateApp;
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

        public async Task MarkAsSeen(int id)
        {
            var report = await _context.Report.FindAsync(id);
            report.Seen = true;
            await _context.SaveChangesAsync();
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

        public override IQueryable<Database.Report> AddFilter(IQueryable<Database.Report> query, SearchReportRequest search = null)
        {
            if (!string.IsNullOrEmpty(search.Text))
            {
                string searchTextLower = search.Text.ToLower();
                query = query.Where(x => x.AdditionalComment.ToLower().Contains(searchTextLower) ||
                                         x.ReportReason.Description.ToLower().Contains(searchTextLower));
            }

            query = query.Include(x => x.User)
                         .Include(x => x.ReportReason)
                         .Include(x => x.Post)
                         .Include(x => x.Comment)
                         .Include(x => x.Post.User)
                         .OrderByDescending(x => x.DateCreated);

            return base.AddFilter(query, search);
        }
       

    }
}
