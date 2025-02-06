using Fiap.Hackathon.Common.Shared.Abstractions;

namespace Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Responses
{
    public class MedicoResponse: ResponseBase
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty; 
        public string CPF { get; set; } = string.Empty;
        public string CRM { get; set; } = string.Empty;
        public string Especialidade { get; set; } = string.Empty;
    }
}
