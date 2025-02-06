using Fiap.Hackathon.Common.Shared.Abstractions;
using Fiap.Hackathon.Common.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Hackathon.Medicos.API.Controllers
{
    public abstract class BaseController<TEntity, TRequest>
                  : Controller
           where TEntity : EntityBase
           where TRequest : IRequest


    {
        protected readonly IService<TEntity, TRequest> _service;

        public BaseController(IService<TEntity, TRequest> service)
        {
            _service = service;
        }
        [HttpPost/*, Authorize*/]
        public virtual async Task<IActionResult> InsertAsync(TRequest request)
        {
            return Ok(await _service.InsertAsync(request));
        }

        [HttpPut("{id}")/*, Authorize*/]
        public async Task<IActionResult> UpdateAsync(TRequest request, Guid id)
        {
            request.Id = id;
            return Ok(await _service.UpdateAsync(request));
        }

        [HttpDelete("{id}"), /*Authorize*/]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Ok(await _service.DeleteAsync(id));
        }

        [HttpGet("{id}"), /*Authorize*/]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }


    }
}
