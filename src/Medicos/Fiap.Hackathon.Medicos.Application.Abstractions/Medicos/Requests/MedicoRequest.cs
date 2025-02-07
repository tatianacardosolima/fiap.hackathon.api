using Fiap.Hackathon.Common.Shared.Interfaces;

namespace Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Requests
{
    public class MedicoRequest : IRequest
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Nome { get; set; } = string.Empty;        
        public string CPF { get; set; } = string.Empty;
        public string CRM { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;
        public List<string> Especialidades { get; set; } = default!;
        public string? Latitude { get; set; }
        public string? Longitude { get;set; }
    }
}
