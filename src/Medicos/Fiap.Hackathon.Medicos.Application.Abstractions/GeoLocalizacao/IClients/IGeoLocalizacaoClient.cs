using Fiap.Hackathon.Medicos.Application.Abstractions.GeoLocalizacao.Responses;

namespace Fiap.Hackathon.Medicos.Application.Abstractions.GeoLocalizacao.IClients
{
    public interface IGeoLocalizacaoClient
    {
        Task<NominatimResponse?> GetLocalizacaoAsync(string latitude, string longitude);
    }
}
