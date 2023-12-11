
using System.ComponentModel;

namespace Friendly.Model.Requests.Report
{
    public class CreateReportRequest
    {
        [DefaultValue(null)]
        public int? PostId { get; set; }

        [DefaultValue(null)]
        public int? CommentId { get; set; }
        public int ReportReasonId { get; set; }
        public string AdditionalComment { get; set; }
    }
}
