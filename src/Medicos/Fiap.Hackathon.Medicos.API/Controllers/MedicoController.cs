using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IServices;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Requests;
using Fiap.Hackathon.Medicos.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Hackathon.Medicos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicoController : BaseController<Medico, MedicoRequest>
    {
        private readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService):base(medicoService) 
        {
            _medicoService = medicoService;
        }

        [HttpGet(), /*Authorize*/]
        public async Task<IActionResult> GetPaginationByFilter(
            [FromQuery] int pagina, [FromQuery] int tamanhoPagina, [FromQuery]string especialidade,
            [FromQuery] string? latitude, [FromQuery] string? longitude)
        {
            return Ok(await _medicoService.GetPaginatedAsync(pagina, tamanhoPagina, especialidade, latitude, longitude));
        }

    }
}
