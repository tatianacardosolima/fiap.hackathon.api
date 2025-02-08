namespace Fiap.Hackathon.Medicos.Application.Abstractions.Autenticacao.Responses
{
    public class UsuarioResponse
    {
        public Guid IdUsuario { get; set; } 

        public string Token { get; set; } = string.Empty;
    }
}
