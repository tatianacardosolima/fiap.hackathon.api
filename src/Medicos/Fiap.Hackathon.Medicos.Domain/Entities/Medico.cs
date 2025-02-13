﻿using Fiap.Hackathon.Common.Shared.Abstractions;
using Fiap.Hackathon.Common.Shared.Shared.Exceptions;
using Fiap.Hackathon.Medicos.Domain.Helpers;


namespace Fiap.Hackathon.Medicos.Domain.Entities
{
    public class Medico : EntityBase
    {
        public Medico(string nome, string cpf, string crm, string email, string senha, List<string> especialidades):base()
        {
            Nome = nome;            
            CPF =  cpf;
            CRM = crm;
            Email = email;
            Senha = senha;
            Especialidades = especialidades;
        }
        
        public string Nome { get; private set; } = string.Empty;        
        public string CPF { get; private set; } = string.Empty;
        public string CRM { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Senha { get; private set; } = string.Empty;
        public List<string> Especialidades { get; private set; } = default!;

        public string? Latitude { get; private set; }
        public string? Longitude { get; private set; }

        public void Change(string nome,string email, List<string> especialidades) 
        {
            Nome = nome;            
            Email = email;            
            Especialidades = especialidades;
        }

        public void SetLatitudeAndLongitude(string latitude, string longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        public void HashSenha()
        { 
            Senha = PasswordHelper.HashPassword(Senha);
        }

        public void HashCPF()
        {
            CPF = HashHelper.GerarHash(CPF);
        }
    }
}
