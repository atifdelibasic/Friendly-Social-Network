using AutoMapper;
using Friendly.Model.Requests.Feedback;
using Friendly.Model.Requests.RateApp;

namespace Friendly.Service
{
    public class FeedbackService : BaseCRUDService<Model.Feedback, Database.Feedback, SearchFeedbackRequest, CreateFeedbackRequest, UpdateFeedbackRequest>, IFeedbackService
    {

        public FeedbackService(Database.FriendlyContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
