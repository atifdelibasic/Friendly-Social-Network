
using Friendly.Model.Requests.Country;
using Friendly.Model.SearchObjects;

namespace Friendly.Service
{
    public interface ICountryService : ICRUDService<Model.Country, SearchCountryObject, CreateCountryRequest, UpdateCountryRequest>
    {
        Task SoftDelete(int cityId, bool isDeleted);

    }
}
