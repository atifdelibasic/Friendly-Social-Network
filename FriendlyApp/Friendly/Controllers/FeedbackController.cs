using Friendly.Model.Requests.Feedback;
using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class FeedbackController : BaseCRUDController<Model.Feedback, SearchFeedbackRequest, CreateFeedbackRequest, UpdateFeedbackRequest>
    {
        public FeedbackController(IFeedbackService service) : base(service)
        {

        }
    }
}
