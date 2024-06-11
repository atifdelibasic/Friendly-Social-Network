using AutoMapper;
using Friendly.Model.Requests.RateApp;
using Friendly.Model.SearchObjects;
using Microsoft.EntityFrameworkCore;

namespace Friendly.Service
{
    public class RateAppService : BaseCRUDService<Model.RateApp, Database.RateApp, SearchRateAppRequest, CreateRateAppRequest, UpdateRateAppRequest>, IRateAppService
    {
        public RateAppService(Database.FriendlyContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override IQueryable<Database.RateApp> AddFilter(IQueryable<Database.RateApp> query, SearchRateAppRequest search = null)
        {
            if (!string.IsNullOrEmpty(search.Text))
            {
                string searchTextLower = search.Text.ToLower();
                query = query.Where(x => (x.Rating).ToString().ToLower().Contains(search.Text));
            }
            query = query.Include(x => x.User).Include(x => x.User);
            query = query.OrderByDescending(x => x.DateCreated);

            return base.AddFilter(query, search);
        }
    }
}
