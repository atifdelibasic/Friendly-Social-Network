using AutoMapper;
using Friendly.Database;
using Friendly.Model.Requests.City;
using Friendly.Model.SearchObjects;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

            query = query.OrderByDescending(x => x.DateCreated);


            return base.AddFilter(query, search);
        }

        public override IQueryable<City> AddInclude(IQueryable<City> query, SearchCityRequest search = null)
        {
            query = query.Include(x => x.Country);


            return base.AddInclude(query, search);
        }

        public async Task<List<Model.City>> GetCities(SearchCityRequest search)
        {
            var query = _context.City.AsQueryable();

            query = query.Where(x => x.DeletedAt == null);

            if (!string.IsNullOrEmpty(search.Text))
            {
                string searchTextLower = search.Text.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(searchTextLower));
            }

            if (search.CountryId.HasValue)
            {
                query = query.Where(x => x.CountryId == search.CountryId.Value);
            }

            query = query.OrderBy(x => x.Name)
                         .AsNoTracking()
                         .Include(x => x.Country);

            var cities = await query.ToListAsync();

            return _mapper.Map<List<Model.City>>(cities);
        }

        public async Task SoftDelete(int id, bool isDeleted)
        {
            City city = await _context.City.FindAsync(id);
            if(isDeleted)
            {
                city.DeletedAt = DateTime.Now;
            }
            else
            {
                city.DeletedAt = null;
            }

            await _context.SaveChangesAsync();
        }
    }
}
