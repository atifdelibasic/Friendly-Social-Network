using AutoMapper;
using Friendly.Model.Requests.Country;
using Friendly.Model.SearchObjects;

namespace Friendly.Service
{
    public class CountryService : BaseCRUDService<Model.Country, Database.Country, SearchCountryObject, CreateCountryRequest, UpdateCountryRequest>, ICountryService
    {
        public CountryService(Database.FriendlyContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override IQueryable<Database.Country> AddFilter(IQueryable<Database.Country> query, SearchCountryObject search = null)
        {
            if (!string.IsNullOrEmpty(search.Text))
            {
                string searchTextLower = search.Text.ToLower();
                query = query.Where(x => (x.Name).ToLower().Contains(search.Text));
            }

            return base.AddFilter(query, search);
        }
    }
}
