using AutoMapper;
using Friendly.Database;
using Friendly.Model.Requests.Country;
using Friendly.Model.SearchObjects;
using Microsoft.EntityFrameworkCore;

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
            query = query.OrderByDescending(x => x.DateCreated);

            return base.AddFilter(query, search);
        }

        public async Task SoftDelete(int id, bool isDeleted)
        {
            Country city = await _context.Country.FindAsync(id);
            if (isDeleted)
            {
                city.DeletedAt = DateTime.Now;
            }
            else
            {
                city.DeletedAt = null;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Model.Country>> GetCountries(SearchCountryObject search)
        {
            var query = _context.City.AsQueryable();
            query = query.Where(x => x.DeletedAt == null);

            if (!string.IsNullOrEmpty(search.Text))
            {
                string searchTextLower = search.Text.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(searchTextLower));
            }

            query = query.OrderBy(x => x.Name)
                         .AsNoTracking()
                         .Include(x => x.Country);

            var cities = await query.ToListAsync();

            return _mapper.Map<List<Model.Country>>(cities);
        }
    }
}
