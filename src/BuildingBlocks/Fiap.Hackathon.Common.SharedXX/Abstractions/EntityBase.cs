using Fiap.Hackathon.Common.Shared.Interfaces;
using Fiap.Hackathon.Common.Shared.Records;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Hackathon.Common.Shared.Abstractions
{
    public abstract class EntityBase: IEntity
    {
        public Guid Id { get; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? ModifiedAt { get; protected set; }
        public bool Active { get; protected set; } = true;

        protected EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;
            Active = true;
        }

        public bool IsActive()
        {
            return Active;
        }
        public void Inactivate()
        {
            //EntityInactiveException.ThrowWhenIsInactive(this, "The entity has already been deleted");
            Active = false;
            ModifiedAt = DateTime.UtcNow;
        }

        public abstract ResponseBase GetResponse();

        [NotMapped]
        protected List<ErrorRecord> _errors = new List<ErrorRecord>();
        [NotMapped]
        public IReadOnlyCollection<ErrorRecord> Errors => _errors;
        public abstract bool Validate();
    }
}
