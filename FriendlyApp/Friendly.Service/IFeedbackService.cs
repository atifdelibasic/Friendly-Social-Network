using Friendly.Model.Requests.Feedback;
using System.Threading.Tasks;

namespace Friendly.Service
{
    public interface IFeedbackService : ICRUDService<Model.Feedback, SearchFeedbackRequest, CreateFeedbackRequest, UpdateFeedbackRequest>
    {
    }
}
