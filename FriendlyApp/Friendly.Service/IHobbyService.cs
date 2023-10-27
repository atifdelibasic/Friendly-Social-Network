using Friendly.Model.Requests.Hobby;

namespace Friendly.Service
{
    public interface IHobbyService : ICRUDService<Model.Hobby, SearchHobbyRequest, CreateHobbyRequest, UpdateHobbyRequest>
    {

    }
}
