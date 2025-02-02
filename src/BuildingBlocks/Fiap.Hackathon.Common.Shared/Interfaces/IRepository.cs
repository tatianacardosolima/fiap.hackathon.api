using System.Linq.Expressions;

namespace Fiap.Hackathon.Common.Shared.Interfaces
{
    public interface IRepository<T, in TId>
           where T : IEntity
           where TId : struct           
    {
        Task InsertAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> GetByIdAsync(TId id);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
        Task SaveChangesAsync();
    }
}
