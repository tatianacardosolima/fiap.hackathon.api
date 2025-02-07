using Fiap.Hackathon.Common.Shared.Abstractions;
using Fiap.Hackathon.Common.Shared.Shared.Exceptions;
using Fiap.Hackathon.Medicos.Domain.Helpers;


namespace Fiap.Hackathon.Medicos.Domain.Entities
{
    public class Medico : EntityBase
    {
        public Medico(string nome, string cpf, string crm, string email, string senha, string especialidade):base()
        {
            Nome = nome;            
            CPF =  cpf;
            CRM = crm;
            Email = email;
            Senha = senha;
            Especialidade = especialidade;
        }
        
        public string Nome { get; private set; } = string.Empty;        
        public string CPF { get; private set; } = string.Empty;
        public string CRM { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Senha { get; private set; } = string.Empty;
        public string Especialidade { get; private set; } = string.Empty;

        public string Latitude { get; private set; }
        public string Longitude { get; private set; }

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
            CPF = PasswordHelper.HashPassword(CPF);
        }
    }
}
