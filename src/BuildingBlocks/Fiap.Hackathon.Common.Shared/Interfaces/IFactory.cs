using Fiap.Hackathon.Common.Shared.Abstractions;

namespace Fiap.Hackathon.Common.Shared.Interfaces
{
    public interface IFactory<TRequest, TEntity> 
            where TRequest: IRequest 
            where TEntity : EntityBase

    {
        Task<TEntity> CreateAsync(TRequest request);
    }
}
