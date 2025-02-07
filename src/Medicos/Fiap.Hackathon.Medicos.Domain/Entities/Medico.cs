using Fiap.Hackathon.Common.Shared.Abstractions;

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

        
        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
