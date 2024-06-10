
using AutoMapper;
using Friendly.Database;
using Friendly.Model;
using Friendly.Model.Requests.ReportReason;
using Microsoft.EntityFrameworkCore;

namespace Friendly.Service
{
    public class ReportReasonService : IReportReasonService
    {
        private readonly IMapper _mapper;
        private readonly FriendlyContext _context;
        private readonly HttpAccessorHelperService _httpAccessorHelper;
        public ReportReasonService(FriendlyContext context, IMapper mapper, HttpAccessorHelperService httpAccessorHelper)
        {
            {
                _mapper = mapper;
                _context = context;
                _httpAccessorHelper = httpAccessorHelper;
            }
        }

        public async Task<List<Model.ReportReason>> GetReportReason(ReportReasonRequest request)
        {
            List<Database.ReportReason> reasons = await _context.ReportReason.ToListAsync();

            return _mapper.Map<List<Model.ReportReason>>(reasons);
        }
    }
}
