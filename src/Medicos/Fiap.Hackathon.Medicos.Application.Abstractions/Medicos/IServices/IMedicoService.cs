using Fiap.Hackathon.Common.Shared.Interfaces;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Requests;
using Fiap.Hackathon.Medicos.Domain.Entities;

namespace Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IServices
{
    public interface IMedicoService : IService<Medico,  MedicoRequest>
    {
    }
}
