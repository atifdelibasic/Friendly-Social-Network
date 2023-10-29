using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "User")]
    public class BaseReadController<T, TSearch> : ControllerBase where T : class where TSearch : class
    {
        protected IReadService<T, TSearch> _service;
        public BaseReadController(IReadService<T, TSearch> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<IEnumerable<T>> Get([FromQuery]TSearch search = null)
        {
            return await _service.Get(search);
        }

        [HttpGet("{id}")]
        public virtual async Task<T> GetById(int id)
        {
            var entity = await _service.GetById(id);

            return entity;
        }
    }
}
