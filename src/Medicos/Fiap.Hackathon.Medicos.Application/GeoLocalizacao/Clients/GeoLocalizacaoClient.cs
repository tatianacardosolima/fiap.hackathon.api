using Fiap.Hackathon.Common.Shared.Shared.Exceptions;
using Fiap.Hackathon.Medicos.Application.Abstractions.GeoLocalizacao.IClients;
using Fiap.Hackathon.Medicos.Application.Abstractions.GeoLocalizacao.Responses;
using System.Net.Http;
using System.Text.Json;

namespace Fiap.Hackathon.Medicos.Application.GeoLocalizacao.Clients
{
    
    public class GeoLocalizacaoClient : IGeoLocalizacaoClient
    {
        private readonly HttpClient _httpClient;
        public GeoLocalizacaoClient(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<NominatimResponse?> GetLocalizacaoAsync(string latitude, string longitude)
        {
            string url = $"reverse?format=json&lat={latitude}&lon={longitude}";

            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Fiap.Hackthon/1.0 (tatidornel@gmail.com)");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var response = await _httpClient.GetAsync(url);
            DomainException.ThrowWhen(!response.IsSuccessStatusCode, "Não foi possível buscar na localização");                        

            var json = await response.Content.ReadAsStringAsync();
            var locationData = JsonSerializer.Deserialize<NominatimResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return locationData;
        }
    }
}
