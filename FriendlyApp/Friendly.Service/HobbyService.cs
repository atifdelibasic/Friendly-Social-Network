using AutoMapper;
using Friendly.Model.Requests.Hobby;

namespace Friendly.Service
{
    public class HobbyService : BaseCRUDService<Model.Hobby, Database.Hobby, SearchHobbyRequest, CreateHobbyRequest, UpdateHobbyRequest> ,IHobbyService
    {
        public HobbyService(Database.FriendlyContext context, IMapper mapper):base(context, mapper)
        {

        }
    }
}
