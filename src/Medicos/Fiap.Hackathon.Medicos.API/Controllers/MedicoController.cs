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
                
        public MedicoController(IMedicoService medicoService):base(medicoService) 
        {            
        }

        
    }
}
