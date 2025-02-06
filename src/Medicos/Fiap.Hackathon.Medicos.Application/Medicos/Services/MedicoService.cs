using Fiap.Hackathon.Common.Shared.Abstractions;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IFactories;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IRepositories;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IServices;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Requests;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Responses;
using Fiap.Hackathon.Medicos.Domain.Entities;

namespace Fiap.Hackathon.Medicos.Application.Medicos.Services
{
    public class MedicoService : ServiceBase<Medico, MedicoRequest, MedicoResponse>, IMedicoService
    {
        public MedicoService(IMedicoRepository repository, IMedicoFactory factory) : base(repository, factory)
        {
        }
    }
}
