using Fiap.Hackathon.Medicos.Application.Abstractions.Autenticacao.IClients;
using Fiap.Hackathon.Medicos.Application.Abstractions.Autenticacao.Requests;
using Fiap.Hackathon.Medicos.Application.Abstractions.Autenticacao.Responses;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Fiap.Hackathon.Medicos.Application.Autenticacao
{
    public class AutenticacaoClient : IAutenticacaoClient
    {
        private readonly HttpClient _httpClient;
        public AutenticacaoClient(HttpClient client)
        {
            _httpClient = client;
        }
        public async  Task<TokenRequest> GetServiceToken(TokenServicoRequest request)
        {
           
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"autenticacoes-servicos", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<SaidaPadrao<TokenRequest>>();
                return result?.Dados!; // Acessando a propriedade Data da resposta padrão
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao autenticar: {response.StatusCode} - {errorMessage}");
            }
        }

        public async Task<UsuarioResponse> SaveAsync(UsuarioRequest request)
        {
            var token = await GetServiceToken(new TokenServicoRequest()
            {
                ClientId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                ClientSecret = "M@sterK3y!2025"
            });

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("usuarios", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<SaidaPadrao<UsuarioResponse>>();
                return result?.Dados!; // Acessando a propriedade Dados da resposta padrão
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao autenticar: {response.StatusCode} - {errorMessage}");
            }
        }

        public async Task<bool> VerifyToken(string token)
        {
            var request = new TokenRequest { Token = token };
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/validacoes-tokens", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<SaidaPadrao>();
                return result?.Sucesso ?? false; // Retorna se a validação foi bem-sucedida
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao validar token: {response.StatusCode} - {errorMessage}");
            }
        }
    }
}
