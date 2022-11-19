using AutoMapper;
using Friendly.Model.Requests.HobbyCategory;

namespace Friendly.Service
{
    public class HobbyCategoryService: BaseCRUDService<Model.HobbyCategory, Database.HobbyCategory, SearchHobbyCategoryRequest, CreateHobbyCategoryRequest, UpdateHobbyCategoryRequest>, IHobbyCategoryService
    {
        public HobbyCategoryService(Database.FriendlyContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
