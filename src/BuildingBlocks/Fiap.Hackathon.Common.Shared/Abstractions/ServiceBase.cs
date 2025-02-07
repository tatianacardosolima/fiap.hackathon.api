using Fiap.Hackathon.Common.Shared.Exceptions;
using Fiap.Hackathon.Common.Shared.Interfaces;
using Fiap.Hackathon.Common.Shared.Responses;
using Fiap.Hackathon.Common.Shared.Shared.Exceptions;

namespace Fiap.Hackathon.Common.Shared.Abstractions
{
    public class ServiceBase <TEntity, TRequest,TResponse> 
        where TEntity : EntityBase
        where TRequest : IRequest
        where TResponse: ResponseBase<TEntity>, new()

    {
        protected readonly IBaseRepository<TEntity> _repository;
        protected readonly IFactory<TRequest, TEntity> _factory;

        public ServiceBase(IBaseRepository<TEntity> repository,
            IFactory<TRequest, TEntity> factory)
        {
            _repository = repository;
            _factory = factory;
        }

        public virtual async Task<DefaultResponse> InsertAsync(TRequest request)
        {

            var entity = await _factory.CreateAsync(request);
            
            await _repository.AddAsync(entity);
            
            
            return new DefaultResponse(true, "Registro Inserido com sucesso", new { Id = entity.Id});
        }

        public virtual async Task<DefaultResponse> UpdateAsync(TRequest request)
        {

            var entity = await _factory.CreateAsync(request);

            await _repository.UpdateAsync(entity.Id, entity);
            

            return new DefaultResponse(true, "Registro Alterado com sucesso", new { Id = entity.Id });
        }

        public virtual async Task<DefaultResponse> DeleteAsync(Guid id)
        {

            TEntity entity = await _repository.GetByIdAsync(id);
            DomainException.ThrowWhen(entity == null, "Registro não encontrado");
            
            await _repository.DeleteAsync(id);
                        
            return new DefaultResponse(true, "Registro excluído");
        }

        public virtual async Task<DefaultResponse> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            NotFoundException.ThrowWhenNullEntity(entity, "Registro não encontrado");
            TResponse response = new();            
            return new DefaultResponse(true, "Registro encontrado", response.GetResponse(entity));
        }

       
    }
}
