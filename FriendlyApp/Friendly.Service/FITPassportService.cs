
using AutoMapper;
using Friendly.Database;
using Friendly.Model.Requests.Comment;
using Friendly.Model.Requests.FITPassport;
using Friendly.Model.SearchObjects;
using Microsoft.EntityFrameworkCore;

namespace Friendly.Service
{
    public class FITPassportService : BaseCRUDService<Model.FITPassport, Database.FITPassport, SearchFITPassportRequest, CreateFITPassportRequest, UpdateFITPassportRequest>, IFITPassport
    {
        public FITPassportService(Database.FriendlyContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override IQueryable<Database.FITPassport> AddInclude(IQueryable<Database.FITPassport> query, SearchFITPassportRequest search = null)
        {
            query = query.Include(x => x.User);

            return base.AddInclude(query, search);
        }
        public override IQueryable<Database.FITPassport> AddFilter(IQueryable<Database.FITPassport> query, SearchFITPassportRequest? search = null)
        {
            if (!string.IsNullOrEmpty(search.Text))
            {
                string searchTextLower = search.Text.ToLower();
                query = query.Where(x => ((x.User.FirstName + " " + x.User.LastName).ToLower().Contains(search.Text)) || (x.Id.ToString() == search.Text) || (x.User.Id.ToString() == search.Text));

            }
            query = query.OrderByDescending(x => x.DateCreated);

            return base.AddFilter(query, search);
        }
    }
}
