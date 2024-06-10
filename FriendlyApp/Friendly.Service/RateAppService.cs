using AutoMapper;
using Friendly.Model.Requests.RateApp;

namespace Friendly.Service
{
    public class RateAppService : BaseCRUDService<Model.RateApp, Database.RateApp, SearchRateAppRequest, CreateRateAppRequest, UpdateRateAppRequest>, IRateAppService
    {
        public RateAppService(Database.FriendlyContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
