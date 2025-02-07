namespace Fiap.Hackathon.Common.Shared.Abstractions
{
    public class PaginatedResponse<T>
    {
        public long QuantidadeItens { get; set; }
        public int QuantidadePaginas { get; set; }
        public int PaginaAtual { get; set; }
        public int TamanhoPagina { get; set; }
        public List<T> Dados { get; set; } = new();
    }

}
