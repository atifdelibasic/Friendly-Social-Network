using AutoMapper;
using Friendly.Database;
using Friendly.Model.Requests.Hobby;

namespace Friendly.Service
{
    public class HobbyService : BaseCRUDService<Model.Hobby, Database.Hobby, SearchHobbyRequest, CreateHobbyRequest, UpdateHobbyRequest> ,IHobbyService
    {
        public HobbyService(Database.FriendlyContext context, IMapper mapper):base(context, mapper)
        {

        }

        public override IQueryable<Hobby> AddFilter(IQueryable<Hobby> query, SearchHobbyRequest search = null)
        {
            if (search.HobbyCategoryId != null)
            {
                query = query.Where(x => x.HobbyCategoryId == search.HobbyCategoryId);
            }

            if (!string.IsNullOrEmpty(search.Text))
            {
                string searchTextLower = search.Text.ToLower();
                query = query.Where(x => x.Title.ToLower().Contains(searchTextLower) || x.Description.ToLower().Contains(searchTextLower));
            }

           
            return base.AddFilter(query, search);
        }
    }
}
