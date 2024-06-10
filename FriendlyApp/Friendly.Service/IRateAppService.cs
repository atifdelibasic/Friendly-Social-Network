using Friendly.Model.Requests.Feedback;
using Friendly.Model.Requests.RateApp;

namespace Friendly.Service
{
    public interface IRateAppService : ICRUDService<Model.RateApp, SearchRateAppRequest, CreateRateAppRequest, UpdateRateAppRequest>
    {
    }
}
