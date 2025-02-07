using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IFactories;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Requests;
using Fiap.Hackathon.Medicos.Domain.Entities;

namespace Fiap.Hackathon.Medicos.Application.Medicos.Factories
{
    public class MedicoFactory : IMedicoFactory
    {
        public Task<Medico> CreateAsync(MedicoRequest request)
        {
            var medico = new Medico(request.Nome, request.CPF, request.CRM, request.Email, request.Senha, request.Especialidade);
            return Task.FromResult(medico);
        }
    }
}
