using Fiap.Hackathon.Medicos.Application.Abstractions.Autenticacao.Requests;
using Fiap.Hackathon.Medicos.Application.Abstractions.Autenticacao.Responses;

namespace Fiap.Hackathon.Medicos.Application.Abstractions.Autenticacao.IClients
{
    public interface IAutenticacaoClient
    {
        Task<UsuarioResponse> SaveAsync(UsuarioRequest request);

        Task<bool> VerifyToken(string token);

        Task<TokenRequest> GetServiceToken(TokenServicoRequest request);
    }
}
