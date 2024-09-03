using Friendly.Model.Requests.FITPassport;
using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    //[Authorize]
    public class FitController : BaseCRUDController<Model.FITPassport, SearchFITPassportRequest, CreateFITPassportRequest, UpdateFITPassportRequest>
    {
        private readonly IFITPassport _service;

        public FitController(IFITPassport service) : base(service)
        {
            _service = service;
        }
    }
}
