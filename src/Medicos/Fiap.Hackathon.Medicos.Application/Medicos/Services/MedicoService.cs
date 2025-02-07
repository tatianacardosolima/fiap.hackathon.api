using Fiap.Hackathon.Common.Shared.Abstractions;
using Fiap.Hackathon.Common.Shared.Interfaces;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IFactories;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IRepositories;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IServices;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Requests;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Responses;
using Fiap.Hackathon.Medicos.Domain.Entities;
using MongoDB.Driver;

namespace Fiap.Hackathon.Medicos.Application.Medicos.Services
{
    public class MedicoService : ServiceBase<Medico, MedicoRequest, MedicoResponse>, IMedicoService
    {
        public MedicoService(IMedicoRepository repository, IMedicoFactory factory) : base(repository, factory)
        {
        }
        public virtual async Task<PaginatedResponse<MedicoResponse>> GetPaginatedAsync(
           int pagina, int tamanhoPagina, string especialidade)
        {
            var filtro = Builders<Medico>.Filter.Empty; // Sem filtro retorna tudo

            // Exemplo: Filtrar médicos por especialidade
            if (!string.IsNullOrEmpty(especialidade))
            {
                filtro &= Builders<Medico>.Filter.Eq(m => m.Especialidade, especialidade);
            }
            var resultadoEntity = await _repository.GetPaginatedAsync(pagina, tamanhoPagina, filtro);
            PaginatedResponse<MedicoResponse> resultado = new()
            {
                PaginaAtual = resultadoEntity.PaginaAtual,
                QuantidadeItens = resultadoEntity.QuantidadeItens,
                QuantidadePaginas = resultadoEntity.QuantidadePaginas,
                TamanhoPagina = resultadoEntity.TamanhoPagina,
                Dados = resultadoEntity.Dados.Select(x => new MedicoResponse()
                {
                    CPF = x.CPF,
                    CRM = x.CRM,
                    Especialidade = x.Especialidade,
                    Nome = x.Nome,                    
                    Id = x.Id
                }).ToList()
            };
            
            return resultado;
        }
      
    }
}
