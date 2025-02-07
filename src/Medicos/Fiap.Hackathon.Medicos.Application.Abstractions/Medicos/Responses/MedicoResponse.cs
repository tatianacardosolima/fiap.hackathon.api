using Fiap.Hackathon.Common.Shared.Abstractions;
using Fiap.Hackathon.Medicos.Domain.Entities;

namespace Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Responses
{
    public class MedicoResponse : ResponseBase<Medico>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;        
        //public string CPF { get; set; } = string.Empty;
        public string CRM { get; set; } = string.Empty;
        public List<string> Especialidades { get; set; } = default!;
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public double? DistanciaKm { get; set; }

        public override MedicoResponse GetResponse(Medico entity)
        {
            return new MedicoResponse()
            {
                Id = entity.Id,
                Nome = entity.Nome,                                
                CRM = entity.CRM,
                Especialidades = entity.Especialidades,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude
            };

        }
    }
}