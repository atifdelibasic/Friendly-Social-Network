using Friendly.Model.Requests.Hobby;

namespace Friendly.Service
{
    public interface IHobbyService : ICRUDService<Model.Hobby, SearchHobbyRequest, CreateHobbyRequest, UpdateHobbyRequest>
    {
        Task<List<Model.Hobby>> GetHobbiesByIds(List<int> hobbyIds);
    }
}
