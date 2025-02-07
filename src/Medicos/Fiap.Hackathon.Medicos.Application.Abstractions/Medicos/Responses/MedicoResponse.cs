using Fiap.Hackathon.Common.Shared.Abstractions;
using Fiap.Hackathon.Common.Shared.Interfaces;
using Fiap.Hackathon.Medicos.Domain.Entities;

namespace Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Responses
{
    public class MedicoResponse : ResponseBase<Medico>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;        
        public string CPF { get; set; } = string.Empty;
        public string CRM { get; set; } = string.Empty;
        public string Especialidade { get; set; } = string.Empty;

        public override MedicoResponse GetResponse(Medico entity)
        {
            return new MedicoResponse()
            {
                Id = entity.Id,
                Nome = entity.Nome,                
                CPF = entity.CPF,
                CRM = entity.CRM,
                Especialidade = entity.Especialidade
            };

        }
    }
}