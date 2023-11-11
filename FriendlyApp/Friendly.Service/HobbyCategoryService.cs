using AutoMapper;
using Friendly.Database;
using Friendly.Model.Requests.HobbyCategory;

namespace Friendly.Service
{
    public class HobbyCategoryService: BaseCRUDService<Model.HobbyCategory, Database.HobbyCategory, SearchHobbyCategoryRequest, CreateHobbyCategoryRequest, UpdateHobbyCategoryRequest>, IHobbyCategoryService
    {
        public HobbyCategoryService(Database.FriendlyContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override IQueryable<HobbyCategory> AddFilter(IQueryable<HobbyCategory> query, SearchHobbyCategoryRequest search = null)
        {
            if (!string.IsNullOrEmpty(search.Text))
            {
                string searchTextLower = search.Text.ToLower();
                query = query.Where(x => x.Name.Contains(searchTextLower));
            }

            return base.AddFilter(query, search);
        }
    }
}
