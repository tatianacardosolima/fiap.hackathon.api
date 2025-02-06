using Fiap.Hackathon.Common.Shared.Interfaces;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Requests;
using Fiap.Hackathon.Medicos.Domain.Entities;

namespace Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IFactories
{
    public interface IMedicoFactory: IFactory<MedicoRequest, Medico>
    {        
    }
}
