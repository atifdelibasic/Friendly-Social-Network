using AutoMapper;
using Friendly.Model.Requests.Hobby;
using Microsoft.EntityFrameworkCore;

namespace Friendly.Service
{
    public class HobbyService : BaseCRUDService<Model.Hobby, Database.Hobby, SearchHobbyRequest, CreateHobbyRequest, UpdateHobbyRequest> ,IHobbyService
    {
        public HobbyService(Database.FriendlyContext context, IMapper mapper):base(context, mapper)
        {

        }

        public async Task<List<Model.Hobby>> GetHobbiesByIds(List<int> hobbyIds)
        {
            List<Database.Hobby> hobbies = await _context.Hobby
                       .Where(h => hobbyIds.Contains(h.Id)).ToListAsync();
                       

            return _mapper.Map<List<Model.Hobby>>(hobbies);
        }

        public override IQueryable<Database.Hobby> AddFilter(IQueryable<Database.Hobby> query, SearchHobbyRequest search = null)
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
