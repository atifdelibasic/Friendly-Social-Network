using Friendly.Model.Requests.HobbyCategory;

namespace Friendly.Service
{
    public interface IHobbyCategoryService: ICRUDService<Model.HobbyCategory, SearchHobbyCategoryRequest, CreateHobbyCategoryRequest, UpdateHobbyCategoryRequest>
    {
        Task  DeleteHobbyCategory(int id);
    }


}
