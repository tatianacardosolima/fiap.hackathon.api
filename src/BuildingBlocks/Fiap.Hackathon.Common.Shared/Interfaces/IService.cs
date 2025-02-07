using Fiap.Hackathon.Common.Shared.Abstractions;
using Fiap.Hackathon.Common.Shared.Responses;

namespace Fiap.Hackathon.Common.Shared.Interfaces
{
    public interface IService<TEntity, TRequest>
        where TEntity : EntityBase
        where TRequest : IRequest
    
    {
        Task<DefaultResponse> InsertAsync(TRequest request);
        Task<DefaultResponse> DeleteAsync(Guid uniqueId);
        Task<DefaultResponse> UpdateAsync(TRequest request);
        Task<DefaultResponse> GetByIdAsync(Guid id);
        
    }
}
