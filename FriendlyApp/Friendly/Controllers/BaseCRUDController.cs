using Friendly.Service;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    public class BaseCRUDController<T, TSearch, TInsert, TUpdate> : BaseReadController<T, TSearch> where T : class where TSearch : class where TInsert : class where TUpdate : class
    {
        protected ICRUDService<T, TSearch, TInsert, TUpdate> _crudService;
        public BaseCRUDController(ICRUDService<T, TSearch, TInsert, TUpdate> service) : base(service)
        {
            _service = service;
        }

        [HttpPost]
        public virtual T Insert([FromBody] TInsert request)
        {
            return _crudService.Insert(request);
        }

        [HttpPut("{id}")]
        public virtual T Update(int id, [FromBody] TUpdate request)
        {
            return _crudService.Update(id, request);
        }

    }
}
