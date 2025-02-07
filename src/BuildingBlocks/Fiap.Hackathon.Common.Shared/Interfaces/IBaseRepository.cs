using Fiap.Hackathon.Common.Shared.Abstractions;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Fiap.Hackathon.Common.Shared.Interfaces
{
    public interface IBaseRepository<T> where T: EntityBase
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter);
        Task<PaginatedResponse<T>> GetPaginatedAsync(
            int pagina, int tamanhoPagina, FilterDefinition<T>? filtro = null);
        Task AddAsync(T entity);
        Task UpdateAsync(Guid id, T entity);
        Task DeleteAsync(Guid id);
    }
}
