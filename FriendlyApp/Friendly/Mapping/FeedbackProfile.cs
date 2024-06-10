using AutoMapper;
using Friendly.Model.Requests.Feedback;

namespace Friendly.WebAPI.Mapping
{
    public class FeedBackProfile : Profile
    {
        public FeedBackProfile()
        {
            CreateMap<CreateFeedbackRequest, Database.Feedback>();
            CreateMap<UpdateFeedbackRequest, Database.Feedback>();
            CreateMap<Database.Feedback, Model.Feedback>();
        }
    }
}
