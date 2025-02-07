using Fiap.Hackathon.Common.Shared.Shared.Exceptions;
using Fiap.Hackathon.Medicos.Application.Abstractions.GeoLocalizacao.IClients;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IFactories;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IRepositories;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Requests;
using Fiap.Hackathon.Medicos.Application.Validators;
using Fiap.Hackathon.Medicos.Domain.Entities;
using Fiap.Hackathon.Medicos.Domain.Exceptions;
using Fiap.Hackathon.Medicos.Domain.Extensions;
using Fiap.Hackathon.Medicos.Domain.Helpers;

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

                var cpfHash = HashHelper.GerarHash(request.CPF);
                medicoExist = await _repository.FindAsync(x => x.CPF == cpfHash);
                CPFRegisteredException.ThrowWhenCPF(medicoExist.Count() > 0, "CPF já cadastrado");

                medico = new Medico(request.Nome, request.CPF.RemoveMask(), request.CRM, request.Email, request.Senha, request.Especialidade);
            }
            else
            { 
                medico = await _repository.GetByIdAsync(request.Id);
                DomainException.ThrowWhen(medico == null, "Médico não encontrado");
                medico!.Change(request.Nome, request.Email, request.Especialidade);
                
            }

            var validator = new MedicoValidator(request.Id == Guid.Empty);
            var validation = validator.Validate(medico!);
            if (!validation.IsValid)
            {
                var error = validation.Errors.ToList().First();
                throw new DomainException(error.ErrorMessage);
            }

            if (request.Latitude != null && request.Longitude != null)
            {
                if (request.Latitude != medico.Latitude || request.Longitude != medico.Longitude)
                {
                    var address = await _geoLocalizacaoClient.GetLocalizacaoAsync(request.Latitude, request.Longitude);

                    DomainException.ThrowWhen(address == null, "Endereço não encontrado");
                    medico!.SetLatitudeAndLongitude(request.Latitude, request.Longitude);
                }
            }

            if (request.Id == Guid.Empty)
            {
                medico!.HashSenha();
                medico.HashCPF();
            }
            return medico;
        }
    }
}
