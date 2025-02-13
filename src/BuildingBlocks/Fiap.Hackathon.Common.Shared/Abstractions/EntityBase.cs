﻿using Fiap.Hackathon.Common.Shared.Interfaces;
using Fiap.Hackathon.Common.Shared.Records;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Hackathon.Common.Shared.Abstractions
{
    public abstract class EntityBase: IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; private set; }
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

        public void ChangeIdUsuario(Guid id)
        {
            Id = id;
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
        

        [NotMapped]
        protected List<ErrorRecord> _errors = new List<ErrorRecord>();
        [NotMapped]
        public IReadOnlyCollection<ErrorRecord> Errors => _errors;
    }
}
