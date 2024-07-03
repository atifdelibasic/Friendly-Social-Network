using Friendly.Model.Requests.City;
using Friendly.Model.SearchObjects;

namespace Friendly.Service
{
    public interface ICityService : ICRUDService<Model.City, SearchCityRequest, CreateCityRequest, UpdateCityRequest>
    {
        Task SoftDelete(int cityId, bool isDeleted);
        Task<List<Model.City>> GetCities(SearchCityRequest request);
    }
}
