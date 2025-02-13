﻿using Fiap.Hackathon.Common.Shared.Shared.Exceptions;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IFactories;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.Requests;
using Fiap.Hackathon.Medicos.Application.Medicos.Factories;
using Fiap.Hackathon.Medicos.Domain.Helpers;
using FluentAssertions;
using Fiap.Hackathon.Medicos.Domain.Extensions;
using Fiap.Hackathon.Medicos.Application.Abstractions.GeoLocalizacao.IClients;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IRepositories;
using Moq;

namespace Fiap.Hackathon.Medicos.Tests.Factories
{
    public class MedicoFactoryTests
    {
        private readonly Mock<IGeoLocalizacaoClient> _geoLocalizacaoClientMock;
        private readonly Mock<IMedicoRepository> _repositoryMock;
        private readonly IMedicoFactory _medicoFactory;

        public MedicoFactoryTests()
        {
            _geoLocalizacaoClientMock = new Mock<IGeoLocalizacaoClient>();
            _repositoryMock = new Mock<IMedicoRepository>();

            _medicoFactory = new MedicoFactory(_geoLocalizacaoClientMock.Object, _repositoryMock.Object);

        }

        [Fact]
        public async Task Deve_Criar_Medico_Com_Dados_Validos()
        {
            // Arrange
            var request = new MedicoRequest
            {
                Nome = "Dr. João",
                CPF = "123.456.789-09",
                CRM = "123456/SP",
                Email = "joao@email.com",
                Senha = "Senha@123",
                Especialidades = new List<string>() { "cardio" }
            };

            // Act
            var medico = await _medicoFactory.CreateAsync(request);

            
            var theSameHash = HashHelper.GerarHash(request.CPF.RemoveMask());
            // Assert
            medico.Should().NotBeNull();
            medico.Nome.Should().Be(request.Nome);
            medico.CPF.Should().Be(theSameHash);
            medico.CRM.Should().Be(request.CRM);
            medico.Email.Should().Be(request.Email);
            medico.Especialidades.Should().BeEquivalentTo(request.Especialidades);
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Se_CPF_For_Invalido()
        {
            // Arrange
            var request = new MedicoRequest
            {
                Nome = "Dr. João",
                CPF = "11111111111", // CPF inválido
                CRM = "123456/SP",
                Email = "joao@email.com",
                Senha = "Senha@123",
                Especialidades = new List<string> { "Cardio" }
            };

            // Act
            Func<Task> act = async () => await _medicoFactory.CreateAsync(request);

            // Assert
            await act.Should().ThrowAsync<DomainException>()
                .WithMessage("CPF inválido.");
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Se_CRM_For_Invalido()
        {
            // Arrange
            var request = new MedicoRequest
            {
                Nome = "Dr. João",
                CPF = "123.456.789-09",
                CRM = "12/SP", // CRM inválido
                Email = "joao@email.com",
                Senha = "Senha@123",
                Especialidades = new List<string> { "Cardio" }
            };

            // Act
            Func<Task> act = async () => await _medicoFactory.CreateAsync(request);

            // Assert
            await act.Should().ThrowAsync<DomainException>()
                .WithMessage("CRM inválido.");
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Se_Email_Estiver_Vazio()
        {
            // Arrange
            var request = new MedicoRequest
            {
                Nome = "Dr. João",
                CPF = "123.456.789-09",
                CRM = "123456/SP",
                Email = "", // Email vazio
                Senha = "Senha@123",
                Especialidades = new List<string> { "Cardio" }
            };

            // Act
            Func<Task> act = async () => await _medicoFactory.CreateAsync(request);

            // Assert
            await act.Should().ThrowAsync<DomainException>()
                .WithMessage("O campo Email é obrigatório.");
        }

        [Theory]
        [InlineData("email@dominio.com", true)]
        [InlineData("email@dominio.com.br", true)]
        [InlineData("usuario123@email.net", true)]
        [InlineData("user.name+alias@email.org", true)]
        [InlineData("email@.com", false)]
        [InlineData("email@", false)]
        [InlineData("email.com", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void Deve_Validar_Emails_Corretamente(string email, bool esperado)
        {
            // Act
            var resultado = email.EhEmailValido();

            // Assert
            resultado.Should().Be(esperado);
        }
    }
}
