using Fiap.Hackathon.Common.Shared.Shared.Exceptions;
using Fiap.Hackathon.Medicos.Application.Abstractions.GeoLocalizacao.IClients;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IFactories;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IRepositories;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Requests;
using Fiap.Hackathon.Medicos.Application.Validators;
using Fiap.Hackathon.Medicos.Domain.Entities;
using Fiap.Hackathon.Medicos.Domain.Extensions;

namespace Fiap.Hackathon.Medicos.Application.Medicos.Factories
{
    public class MedicoFactory : IMedicoFactory
    {
        private readonly IGeoLocalizacaoClient _geoLocalizacaoClient;
        private readonly IMedicoRepository _repository;
        public MedicoFactory(IGeoLocalizacaoClient geoLocalizacaoClient, IMedicoRepository repository)
        {
            _geoLocalizacaoClient = geoLocalizacaoClient;
            _repository = repository;
        }
        public async Task<Medico> CreateAsync(MedicoRequest request)
        {
            Medico? medico = null;
            if (request.Id == Guid.Empty)
            {
                var medicoExist = await _repository.FindAsync(x => x.CRM == request.CRM);
                DomainException.ThrowWhen(medicoExist.Count() > 0, "CRM já cadastrado");
                medico = new Medico(request.Nome, request.CPF.RemoveMask(), request.CRM, request.Email, request.Senha, request.Especialidade);
            }
            else
            { 
                var medicoExist = await _repository.GetByIdAsync(request.Id);
                if (medicoExist == null)
                {
                    medico = new Medico(request.Nome, request.CPF.RemoveMask(), request.CRM, request.Email, request.Senha, request.Especialidade);
                }
                else
                {
                    medico = medicoExist;
                }
            }

            var validator = new MedicoValidator();
            var validation = validator.Validate(medico!);
            if (!validation.IsValid)
            {
                var error = validation.Errors.ToList().First();
                throw new DomainException(error.ErrorMessage);
            }

            if (request.Latitude != null && request.Longitude != null)
            {
                var address = await _geoLocalizacaoClient.GetLocalizacaoAsync(request.Latitude, request.Longitude);

                DomainException.ThrowWhen(address == null, "Endereço não encontrado");
                medico!.SetLatitudeAndLongitude(request.Latitude, request.Longitude);                    
            }
            medico!.HashSenha();
            medico.HashCPF();

            return medico;
        }
    }
}
