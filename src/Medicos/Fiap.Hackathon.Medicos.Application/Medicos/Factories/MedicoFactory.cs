using Fiap.Hackathon.Common.Shared.Shared.Exceptions;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IFactories;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Requests;
using Fiap.Hackathon.Medicos.Application.Validators;
using Fiap.Hackathon.Medicos.Domain.Entities;
using Fiap.Hackathon.Medicos.Domain.Extensions;

namespace Fiap.Hackathon.Medicos.Application.Medicos.Factories
{
    public class MedicoFactory : IMedicoFactory
    {
        public Task<Medico> CreateAsync(MedicoRequest request)
        {
            var medico = new Medico(request.Nome, request.CPF.RemoveMask(), request.CRM, request.Email, request.Senha, request.Especialidade);

            var validator = new MedicoValidator();
            var validation = validator.Validate(medico);
            if (!validation.IsValid)
            {
                var error = validation.Errors.ToList().First();
                throw new DomainException(error.ErrorMessage);
            }

            return Task.FromResult(medico);
        }
    }
}
