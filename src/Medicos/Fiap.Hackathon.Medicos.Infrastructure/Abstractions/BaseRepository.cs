using Fiap.Hackathon.Common.Shared.Abstractions;
using Fiap.Hackathon.Common.Shared.Interfaces;
using Fiap.Hackathon.Medicos.Infrastructure.Persistence;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Fiap.Hackathon.Medicos.Infrastructure.Abstractions
{
    public class BaseRepository<T> : IBaseRepository<T> where T: EntityBase
    {
        private readonly IMongoCollection<T> _collection;

        public BaseRepository(MongoDbContext dbContext, string collectionName)
        {
            _collection = dbContext.GetCollection<T>(collectionName);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _collection.Find(Builders<T>.Filter.Eq("_id", id.ToString())).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(Guid id, T entity)
        {
            await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", id), entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));
        }

        public async Task<PaginatedResponse<T>> GetPaginatedAsync(int pagina, int tamanhoPagina, FilterDefinition<T>? filtro = null)
        {
            if (pagina < 1) pagina = 1;
            if (tamanhoPagina < 1) tamanhoPagina = 10;

            filtro ??= Builders<T>.Filter.Empty;

            var totalItens = await _collection.CountDocumentsAsync(filtro);

            var dados = await _collection
                .Find(filtro)
                .Skip((pagina - 1) * tamanhoPagina) // Pula os itens das páginas anteriores
                .Limit(tamanhoPagina) // Retorna apenas o número de itens da página atual
                .ToListAsync();

            return new PaginatedResponse<T>
            {
                QuantidadeItens = totalItens,
                QuantidadePaginas = (int)Math.Ceiling((double)totalItens / tamanhoPagina),
                PaginaAtual = pagina,
                TamanhoPagina = tamanhoPagina,
                Dados = dados
            };
        }
    }
}
