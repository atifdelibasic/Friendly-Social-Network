using AutoMapper;
using Friendly.Database;
using Friendly.Model.Requests.City;
using Friendly.Model.SearchObjects;
using Microsoft.EntityFrameworkCore;

namespace Friendly.Service
{
    public class CityService : BaseCRUDService<Model.City, Database.City, SearchCityRequest, CreateCityRequest, UpdateCityRequest>, ICityService
    {
        public CityService(Database.FriendlyContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override IQueryable<Database.City> AddFilter(IQueryable<Database.City> query, SearchCityRequest search = null)
        {
            if (!string.IsNullOrEmpty(search.Text))
            {
                string searchTextLower = search.Text.ToLower();
                query = query.Where(x => (x.Name).ToLower().Contains(search.Text));
            }

            if(search.CountryId is not null)
            {
                query = query.Where(x => x.CountryId == search.CountryId);
            }

            return base.AddFilter(query, search);
        }

        public override IQueryable<City> AddInclude(IQueryable<City> query, SearchCityRequest search = null)
        {
            query = query.Include(x => x.Country);


            return base.AddInclude(query, search);
        }

    }
}
