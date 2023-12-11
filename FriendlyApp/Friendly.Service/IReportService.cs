
using Friendly.Model.Requests.Report;
using Friendly.Model.SearchObjects;

namespace Friendly.Service
{
    public interface IReportService: ICRUDService<Model.Report, SearchReportRequest, CreateReportRequest, UpdateReportRequest>
    {
    }
}
