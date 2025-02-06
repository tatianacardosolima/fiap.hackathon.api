using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IRepositories;
using Fiap.Hackathon.Medicos.Domain.Entities;
using Fiap.Hackathon.Medicos.Infrastructure.Abstractions;
using Fiap.Hackathon.Medicos.Infrastructure.Persistence;
using MongoDB.Driver;

namespace Fiap.Hackathon.Medicos.Infrastructure.Repositories
{
    public class MedicoRepository: BaseRepository<Medico>, IMedicoRepository
    {
        private readonly IMongoCollection<Medico> _users;

        public MedicoRepository(MongoDbContext dbContext)
            : base(dbContext, "Medicos")
        {
            _users = dbContext.GetCollection<Medico>("Medicos");
        }
    }
}
