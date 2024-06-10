
using Friendly.Model.Requests.ReportReason;

namespace Friendly.Service
{
    public interface IReportReasonService
    {
        public Task<List<Model.ReportReason>> GetReportReason(ReportReasonRequest request);
    }
}
