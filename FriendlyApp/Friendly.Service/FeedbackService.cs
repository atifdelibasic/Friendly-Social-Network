using AutoMapper;
using Friendly.Model.Requests.Feedback;
using Microsoft.EntityFrameworkCore;

namespace Friendly.Service
{
    public class FeedbackService : BaseCRUDService<Model.Feedback, Database.Feedback, SearchFeedbackRequest, CreateFeedbackRequest, UpdateFeedbackRequest>, IFeedbackService
    {

        public FeedbackService(Database.FriendlyContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override IQueryable<Database.Feedback> AddFilter(IQueryable<Database.Feedback> query, SearchFeedbackRequest search = null)
        {
            if (!string.IsNullOrEmpty(search.Text))
            {
                string searchTextLower = search.Text.ToLower();
                query = query.Where(x => x.Text.ToLower().Contains(searchTextLower));
            }

            query = query.Include(x => x.User).Include(x => x.User).IgnoreQueryFilters();
            query = query.OrderByDescending(x => x.DateCreated);


            return base.AddFilter(query, search);
        }
    }
}
