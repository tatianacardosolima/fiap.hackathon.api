using Fiap.Hackathon.Common.Shared.Abstractions;
using Fiap.Hackathon.Common.Shared.Interfaces;
using Fiap.Hackathon.Common.Shared.Shared.Exceptions;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IFactories;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IRepositories;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IServices;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Requests;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Responses;
using Fiap.Hackathon.Medicos.Domain.Entities;
using Fiap.Hackathon.Medicos.Domain.Helpers;
using MongoDB.Driver;

namespace Fiap.Hackathon.Medicos.Application.Medicos.Services
{
    public class MedicoService : ServiceBase<Medico, MedicoRequest, MedicoResponse>, IMedicoService
    {
        public MedicoService(IMedicoRepository repository, IMedicoFactory factory) : base(repository, factory)
        {
        }

        public async Task<MedicoResponse> GetByUsuarioAndSenha(string usuario, string senha)
        {
            var usuarioHash = PasswordHelper.HashPassword(usuario);
            var entity = await _repository.FindAsync(x => x.CPF == usuarioHash || x.CRM == usuario);
            DomainException.ThrowWhen(entity == null && entity!.Count() ==0, "Usuário não encontrado");
            
            DomainException.ThrowWhen(!PasswordHelper.VerifyPassword(senha, entity!.FirstOrDefault()!.Senha), "Usuário não encontrado");

            return new MedicoResponse
            {
                CRM = entity!.FirstOrDefault()!.CRM,
                Nome = entity!.FirstOrDefault()!.Nome,
                Id = entity!.FirstOrDefault()!.Id
            };

        }

        public virtual async Task<PaginatedResponse<MedicoResponse>> GetPaginatedAsync(
           int pagina, int tamanhoPagina, string especialidade, string? latitude, string? longitude)
        {
            var filtro = Builders<Medico>.Filter.Empty; // Sem filtro retorna tudo

            // Exemplo: Filtrar médicos por especialidade
            if (!string.IsNullOrEmpty(especialidade))
            {
                filtro &= Builders<Medico>.Filter.Where(m => m.Especialidades.Contains(especialidade));
            }
            

            // Se não tiver coordenadas, retorna a lista padrão
            if (string.IsNullOrEmpty(latitude) || string.IsNullOrEmpty(longitude) ||
                !double.TryParse(latitude, out double latCliente) || !double.TryParse(longitude, out double lonCliente))
            {
                var resultadoEntity = await _repository.GetPaginatedAsync(pagina, tamanhoPagina, filtro);

                PaginatedResponse<MedicoResponse> resultado = new()
                {
                    PaginaAtual = resultadoEntity.PaginaAtual,
                    QuantidadeItens = resultadoEntity.QuantidadeItens,
                    QuantidadePaginas = resultadoEntity.QuantidadePaginas,
                    TamanhoPagina = resultadoEntity.TamanhoPagina,
                    Dados = resultadoEntity.Dados.Select(x => new MedicoResponse()
                    {
                        CRM = x.CRM,
                        Especialidades = x.Especialidades,
                        Nome = x.Nome,
                        Latitude = x.Latitude,
                        Longitude = x.Longitude,
                        Id = x.Id
                    }).ToList()
                };

                return resultado;
            }
            else
            {
                var resultadoEntity = await _repository.FindAsync(x => x.Especialidades.Contains(especialidade) && x.Latitude != null);

                var dadosOrdenados = resultadoEntity
                          .Select(x => new MedicoResponse()
                          {
                              CRM = x.CRM,
                              Especialidades = x.Especialidades,
                              Nome = x.Nome,
                              Id = x.Id,
                              Latitude = x.Latitude,
                              Longitude = x.Longitude,
                              DistanciaKm = x.Latitude == null ? null :
                                                GeoLocalizacaoHelper.CalcularDistancia(latCliente, lonCliente,
                                                                                        double.Parse(x.Latitude!), double.Parse(x.Longitude!))
                          })
                          .OrderBy(m => m.DistanciaKm)
                          .Skip((pagina - 1) * tamanhoPagina)
                          .Take(tamanhoPagina) // Paginação já com médicos mais próximos                                
                          .ToList();

                return new PaginatedResponse<MedicoResponse>
                {
                    PaginaAtual = pagina,
                    QuantidadeItens = resultadoEntity.Count(),
                    QuantidadePaginas = (int)Math.Ceiling((double)resultadoEntity.Count() / tamanhoPagina),
                    TamanhoPagina = dadosOrdenados.Count,
                    Dados = dadosOrdenados
                };
            }
        }

    }
}
